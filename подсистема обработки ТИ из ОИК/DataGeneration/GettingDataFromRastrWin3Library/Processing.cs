using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASTRALib;

namespace GettingDataFromRastrWin3Library
{
    public class Processing
    {
        static Rastr _rastr = new Rastr();

        public static AllData GetDataFromRastrWin3(string path)
        {
            LoadFiles(path);

            _rastr.rgm("");

            var nodes = GetHashOfNodes();

            var edges = GetEdges(nodes);

            var generators = GetHashOfGenerators(nodes);

            return new AllData(nodes, edges, generators);
        }

        private static HashSet<Node> GetHashOfNodes()
        {
            table nodesTable = (table)_rastr.Tables.Item("node");
            var rowsCount = nodesTable.Count;

            col state = (col)nodesTable.Cols.Item("sta");
            col number = (col)nodesTable.Cols.Item("ny");
            col nominalVoltage = (col)nodesTable.Cols.Item("uhom");
            col calculatedVoltage = (col)nodesTable.Cols.Item("vras");
            col activeLoad = (col)nodesTable.Cols.Item("pn");
            col reactiveLoad = (col)nodesTable.Cols.Item("qn");
            col district = (col)nodesTable.Cols.Item("npa");

            HashSet<Node> nodes = new HashSet<Node>();

            for (int i = 0; i < rowsCount; i++)
            {
                if (_districtNumbers.Contains(district.Z[i]))
                {
                    nodes.Add(new Node((state.Z[i] == true) ? 1 : 0, number.Z[i], 
                        nominalVoltage.Z[i], calculatedVoltage.Z[i], 
                        activeLoad.Z[i], reactiveLoad.Z[i], district.Z[i]));
                }
            }

            return nodes;
        }

        private static Edges GetEdges(HashSet<Node> nodes)
        {
            table vetv = (table)_rastr.Tables.Item("vetv");
            var rowsCount = vetv.Count;

            col type = (col)vetv.Cols.Item("tip");
            col state = (col)vetv.Cols.Item("sta");
            col firstNodeNumber = (col)vetv.Cols.Item("ip");
            col lastNodeNumber = (col)vetv.Cols.Item("iq");
            col activePowerAtFirst = (col)vetv.Cols.Item("pl_ip");
            col activePowerInTheEnd = (col)vetv.Cols.Item("pl_iq");
            col reactivePowerAtFirst = (col)vetv.Cols.Item("ql_ip");
            col reactivePowerInTheEnd = (col)vetv.Cols.Item("ql_iq");
            col currentMax = (col)vetv.Cols.Item("i_max");
            col currentAtFirst = (col)vetv.Cols.Item("ib");
            col currentInTheEnd = (col)vetv.Cols.Item("ie");

            HashSet<LineSegment> lineSegments = new HashSet<LineSegment>();
            HashSet<Transformer> transformers = new HashSet<Transformer>();
            HashSet<Switch> switches = new HashSet<Switch>();

            for (int i = 0; i < rowsCount; i++)
            {
                var firestNode = nodes.FirstOrDefault(
                    node => node.Number == firstNodeNumber.Z[i]);
                var lastNode = nodes.FirstOrDefault(
                    node => node.Number == lastNodeNumber.Z[i]);

                if (nodes.Contains(firestNode) && nodes.Contains(lastNode))
                {
                    if (type.Z[i] == 0)
                    {
                        lineSegments.Add(new LineSegment((state.Z[i] == 0) ? 0 : 1, 
                            firestNode, lastNode, firestNode.ControlParametrCoefficient, 
                            activePowerAtFirst.Z[i], activePowerInTheEnd.Z[i], 
                            reactivePowerAtFirst.Z[i], reactivePowerInTheEnd.Z[i], 
                            currentAtFirst.Z[i], currentInTheEnd.Z[i],
                            ConditionMore(firestNode.ControlParametrCoefficient, lastNode.ControlParametrCoefficient)));
                    }
                    else if (type.Z[i] == 1)
                    {
                        transformers.Add(new Transformer((state.Z[i] == 0) ? 0 : 1, 
                            firestNode, lastNode, 
                            ConditionLess(firestNode.ControlParametrCoefficient, lastNode.ControlParametrCoefficient),
                            ConditionMore(activePowerAtFirst.Z[i], activePowerInTheEnd.Z[i]),
                            ConditionMore(reactivePowerAtFirst.Z[i], reactivePowerInTheEnd.Z[i]),
                            currentMax.Z[i],
                            ConditionMore(firestNode.ControlParametrCoefficient, lastNode.ControlParametrCoefficient)));
                    }
                    else
                    {
                        switches.Add(new Switch((state.Z[i] == 0) ? 0 : 1, 
                            firestNode, lastNode, firestNode.ControlParametrCoefficient,
                            ConditionMore(firestNode.ControlParametrCoefficient, lastNode.ControlParametrCoefficient)));
                    }
                }
            }

            return new Edges(lineSegments, transformers, switches);
        }

        private static double ConditionMore(double item1, double item2)
        {
            return (item1 >= item2) ? item1 : item2;
        }

        private static double ConditionLess(double item1, double item2)
        {
            return (item1 <= item2) ? item1 : item2;
        }

        private static HashSet<Generator> GetHashOfGenerators(HashSet<Node> nodes)
        {
            table generatorsTable = (table)_rastr.Tables.Item("Generator");
            var rowsCount = generatorsTable.Count;

            col state = (col)generatorsTable.Cols.Item("sta");
            col numberOfNode = (col)generatorsTable.Cols.Item("Node");
            col activePower = (col)generatorsTable.Cols.Item("P");
            col reactivePower = (col)generatorsTable.Cols.Item("Q");
            col nominalActivePower = (col)generatorsTable.Cols.Item("Pmax");

            HashSet<Generator> generators = new HashSet<Generator>();

            for (int i = 0; i < rowsCount; i++)
            {
                var connectionNode = nodes.FirstOrDefault(
                    node => node.Number == numberOfNode.Z[i]);

                if (nodes.Contains(connectionNode))
                {
                    generators.Add(new Generator((state.Z[i] == true) ? 1 : 0, 
                        connectionNode, activePower.Z[i], reactivePower.Z[i], 
                        nominalActivePower.Z[i], connectionNode.DistrictCoefficient));
                }
            }

            return generators;
        }

        /// <summary>
        /// Загрузка файла режима
        /// </summary>
        /// <param name="rastr">Объект растра</param>
        private static void LoadFiles(string path)
        {
            _rastr.Load(RG_KOD.RG_REPL, path, null);
        }

        private static List<int> _districtNumbers = new List<int>()
        {
            60208,
            60401,
            60402,
            60403,
            60404,
            60405,
            60504,
            60511,
            60512,
            60513,
            60515,
            60516,
            60517,
            60521,
            60522,
            60523,
            60524,
            60525,
            60526,
            60527,
            60528,
            60531,
            60533,
            60534,
            70400
        };
    }
}
