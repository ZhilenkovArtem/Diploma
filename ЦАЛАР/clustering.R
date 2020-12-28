library(dtwclust)
library(e1071)
library(readr)

data_s_U_1 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401031.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_1 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401031.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_1 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401031.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_1 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401031.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_1 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401031.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_1 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401031.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_2 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401037.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_2 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401037.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_2 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401037.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_2 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401037.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_2 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401037.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_2 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401037.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_3 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401041.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_3 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401041.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_3 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401041.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_3 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401041.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_3 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401041.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_3 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401041.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_4 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401057.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_4 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401057.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_4 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401057.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_4 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401057.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_4 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401057.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_4 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401057.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_5 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401059.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_5 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401059.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_5 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401059.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_5 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401059.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_5 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401059.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_5 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401059.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_6 = read.table("O:/VKR/DataForClassifier/Stable/BPP/U/60401084.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_6 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/U/60401084.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_6 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/U/60401084.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_6 = read.table("O:/VKR/DataForClassifier/Stable/BPP/Delta/60401084.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_6 = read.table("O:/VKR/DataForClassifier/UnstableSlow/BPP/Delta/60401084.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_6 = read.table("O:/VKR/DataForClassifier/UnstableFast/BPP/Delta/60401084.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_7 = read.table("O:/VKR/DataForClassifier/Stable/Ozer/U/60401143.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_7 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Ozer/U/60401143.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_7 = read.table("O:/VKR/DataForClassifier/UnstableFast/Ozer/U/60401143.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_7 = read.table("O:/VKR/DataForClassifier/Stable/Ozer/Delta/60401143.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_7 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Ozer/Delta/60401143.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_7 = read.table("O:/VKR/DataForClassifier/UnstableFast/Ozer/Delta/60401143.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_8 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/U/60401173.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_8 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/U/60401173.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_8 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/U/60401173.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_8 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/Delta/60401173.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_8 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/Delta/60401173.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_8 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/Delta/60401173.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_9 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/U/60401174.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_9 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/U/60401174.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_9 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/U/60401174.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_9 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/Delta/60401174.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_9 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/Delta/60401174.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_9 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/Delta/60401174.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_10 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/U/60401175.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_10 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/U/60401175.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_10 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/U/60401175.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_10 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/Delta/60401175.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_10 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/Delta/60401175.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_10 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/Delta/60401175.csv", sep = ",", header = TRUE)[,1:4]

data_s_U_11 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/U/60401176.csv", sep = ",", header = TRUE)[,1:4]
data_ns_U_11 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/U/60401176.csv", sep = ",", header = TRUE)[,1:4]
data_nf_U_11 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/U/60401176.csv", sep = ",", header = TRUE)[,1:4]

data_s_Delta_11 = read.table("O:/VKR/DataForClassifier/Stable/Taishet/Delta/60401176.csv", sep = ",", header = TRUE)[,1:4]
data_ns_Delta_11 = read.table("O:/VKR/DataForClassifier/UnstableSlow/Taishet/Delta/60401176.csv", sep = ",", header = TRUE)[,1:4]
data_nf_Delta_11 = read.table("O:/VKR/DataForClassifier/UnstableFast/Taishet/Delta/60401176.csv", sep = ",", header = TRUE)[,1:4]

data_s_1 = vector("list", nrow(data_s_U_1))
i = 1
while (i < nrow(data_s_U_1) + 1){
data_s_1[[i]] = cbind(as.numeric(data_s_U_1[i,]), as.numeric(data_s_Delta_1[i,]))
i = i + 1
}
data_ns_1 = vector("list", nrow(data_ns_U_1))
i = 1
while (i < nrow(data_ns_U_1) + 1){
data_ns_1[[i]] = cbind(as.numeric(data_ns_U_1[i,]), as.numeric(data_ns_Delta_1[i,]))
i = i + 1
}
data_nf_1 = vector("list", nrow(data_nf_U_1))
i = 1
while (i < nrow(data_nf_U_1) + 1){
data_nf_1[[i]] = cbind(as.numeric(data_nf_U_1[i,]), as.numeric(data_nf_Delta_1[i,]))
i = i + 1
}

data_s_2 = vector("list", nrow(data_s_U_2))
i = 1
while (i < nrow(data_s_U_2) + 1){
data_s_2[[i]] = cbind(as.numeric(data_s_U_2[i,]), as.numeric(data_s_Delta_2[i,]))
i = i + 1
}
data_ns_2 = vector("list", nrow(data_ns_U_2))
i = 1
while (i < nrow(data_ns_U_2) + 1){
data_ns_2[[i]] = cbind(as.numeric(data_ns_U_2[i,]), as.numeric(data_ns_Delta_2[i,]))
i = i + 1
}
data_nf_2 = vector("list", nrow(data_nf_U_2))
i = 1
while (i < nrow(data_nf_U_2) + 1){
data_nf_2[[i]] = cbind(as.numeric(data_nf_U_2[i,]), as.numeric(data_nf_Delta_2[i,]))
i = i + 1
}

data_s_3 = vector("list", nrow(data_s_U_3))
i = 1
while (i < nrow(data_s_U_3) + 1){
data_s_3[[i]] = cbind(as.numeric(data_s_U_3[i,]), as.numeric(data_s_Delta_3[i,]))
i = i + 1
}
data_ns_3 = vector("list", nrow(data_ns_U_3))
i = 1
while (i < nrow(data_ns_U_3) + 1){
data_ns_3[[i]] = cbind(as.numeric(data_ns_U_3[i,]), as.numeric(data_ns_Delta_3[i,]))
i = i + 1
}
data_nf_3 = vector("list", nrow(data_nf_U_3))
i = 1
while (i < nrow(data_nf_U_3) + 1){
data_nf_3[[i]] = cbind(as.numeric(data_nf_U_3[i,]), as.numeric(data_nf_Delta_3[i,]))
i = i + 1
}

data_s_4 = vector("list", nrow(data_s_U_4))
i = 1
while (i < nrow(data_s_U_4) + 1){
data_s_4[[i]] = cbind(as.numeric(data_s_U_4[i,]), as.numeric(data_s_Delta_4[i,]))
i = i + 1
}
data_ns_4 = vector("list", nrow(data_ns_U_4))
i = 1
while (i < nrow(data_ns_U_4) + 1){
data_ns_4[[i]] = cbind(as.numeric(data_ns_U_4[i,]), as.numeric(data_ns_Delta_4[i,]))
i = i + 1
}
data_nf_4 = vector("list", nrow(data_nf_U_4))
i = 1
while (i < nrow(data_nf_U_4) + 1){
data_nf_4[[i]] = cbind(as.numeric(data_nf_U_4[i,]), as.numeric(data_nf_Delta_4[i,]))
i = i + 1
}

data_s_5 = vector("list", nrow(data_s_U_5))
i = 1
while (i < nrow(data_s_U_5) + 1){
data_s_5[[i]] = cbind(as.numeric(data_s_U_5[i,]), as.numeric(data_s_Delta_5[i,]))
i = i + 1
}
data_ns_5 = vector("list", nrow(data_ns_U_5))
i = 1
while (i < nrow(data_ns_U_5) + 1){
data_ns_5[[i]] = cbind(as.numeric(data_ns_U_5[i,]), as.numeric(data_ns_Delta_5[i,]))
i = i + 1
}
data_nf_5 = vector("list", nrow(data_nf_U_5))
i = 1
while (i < nrow(data_nf_U_5) + 1){
data_nf_5[[i]] = cbind(as.numeric(data_nf_U_5[i,]), as.numeric(data_nf_Delta_5[i,]))
i = i + 1
}

data_s_6 = vector("list", nrow(data_s_U_6))
i = 1
while (i < nrow(data_s_U_6) + 1){
data_s_6[[i]] = cbind(as.numeric(data_s_U_6[i,]), as.numeric(data_s_Delta_6[i,]))
i = i + 1
}
data_ns_6 = vector("list", nrow(data_ns_U_6))
i = 1
while (i < nrow(data_ns_U_6) + 1){
data_ns_6[[i]] = cbind(as.numeric(data_ns_U_6[i,]), as.numeric(data_ns_Delta_6[i,]))
i = i + 1
}
data_nf_6 = vector("list", nrow(data_nf_U_6))
i = 1
while (i < nrow(data_nf_U_6) + 1){
data_nf_6[[i]] = cbind(as.numeric(data_nf_U_6[i,]), as.numeric(data_nf_Delta_6[i,]))
i = i + 1
}

data_s_7 = vector("list", nrow(data_s_U_7))
i = 1
while (i < nrow(data_s_U_7) + 1){
data_s_7[[i]] = cbind(as.numeric(data_s_U_7[i,]), as.numeric(data_s_Delta_7[i,]))
i = i + 1
}
data_ns_7 = vector("list", nrow(data_ns_U_7))
i = 1
while (i < nrow(data_ns_U_7) + 1){
data_ns_7[[i]] = cbind(as.numeric(data_ns_U_7[i,]), as.numeric(data_ns_Delta_7[i,]))
i = i + 1
}
data_nf_7 = vector("list", nrow(data_nf_U_7))
i = 1
while (i < nrow(data_nf_U_7) + 1){
data_nf_7[[i]] = cbind(as.numeric(data_nf_U_7[i,]), as.numeric(data_nf_Delta_7[i,]))
i = i + 1
}

data_s_8 = vector("list", nrow(data_s_U_8))
i = 1
while (i < nrow(data_s_U_8) + 1){
data_s_8[[i]] = cbind(as.numeric(data_s_U_8[i,]), as.numeric(data_s_Delta_8[i,]))
i = i + 1
}
data_ns_8 = vector("list", nrow(data_ns_U_8))
i = 1
while (i < nrow(data_ns_U_8) + 1){
data_ns_8[[i]] = cbind(as.numeric(data_ns_U_8[i,]), as.numeric(data_ns_Delta_8[i,]))
i = i + 1
}
data_nf_8 = vector("list", nrow(data_nf_U_8))
i = 1
while (i < nrow(data_nf_U_8) + 1){
data_nf_8[[i]] = cbind(as.numeric(data_nf_U_8[i,]), as.numeric(data_nf_Delta_8[i,]))
i = i + 1
}

data_s_9 = vector("list", nrow(data_s_U_9))
i = 1
while (i < nrow(data_s_U_9) + 1){
data_s_9[[i]] = cbind(as.numeric(data_s_U_9[i,]), as.numeric(data_s_Delta_9[i,]))
i = i + 1
}
data_ns_9 = vector("list", nrow(data_ns_U_9))
i = 1
while (i < nrow(data_ns_U_9) + 1){
data_ns_9[[i]] = cbind(as.numeric(data_ns_U_9[i,]), as.numeric(data_ns_Delta_9[i,]))
i = i + 1
}
data_nf_9 = vector("list", nrow(data_nf_U_9))
i = 1
while (i < nrow(data_nf_U_9) + 1){
data_nf_9[[i]] = cbind(as.numeric(data_nf_U_9[i,]), as.numeric(data_nf_Delta_9[i,]))
i = i + 1
}

data_s_10 = vector("list", nrow(data_s_U_10))
i = 1
while (i < nrow(data_s_U_10) + 1){
data_s_10[[i]] = cbind(as.numeric(data_s_U_10[i,]), as.numeric(data_s_Delta_10[i,]))
i = i + 1
}
data_ns_10 = vector("list", nrow(data_ns_U_10))
i = 1
while (i < nrow(data_ns_U_10) + 1){
data_ns_10[[i]] = cbind(as.numeric(data_ns_U_10[i,]), as.numeric(data_ns_Delta_10[i,]))
i = i + 1
}
data_nf_10 = vector("list", nrow(data_nf_U_10))
i = 1
while (i < nrow(data_nf_U_10) + 1){
data_nf_10[[i]] = cbind(as.numeric(data_nf_U_10[i,]), as.numeric(data_nf_Delta_10[i,]))
i = i + 1
}

data_s_11 = vector("list", nrow(data_s_U_11))
i = 1
while (i < nrow(data_s_U_11) + 1){
data_s_11[[i]] = cbind(as.numeric(data_s_U_11[i,]), as.numeric(data_s_Delta_11[i,]))
i = i + 1
}
data_ns_11 = vector("list", nrow(data_ns_U_11))
i = 1
while (i < nrow(data_ns_U_11) + 1){
data_ns_11[[i]] = cbind(as.numeric(data_ns_U_11[i,]), as.numeric(data_ns_Delta_11[i,]))
i = i + 1
}
data_nf_11 = vector("list", nrow(data_nf_U_11))
i = 1
while (i < nrow(data_nf_U_11) + 1){
data_nf_11[[i]] = cbind(as.numeric(data_nf_U_11[i,]), as.numeric(data_nf_Delta_11[i,]))
i = i + 1
}

fcs1 = tsclust(data_s_1, type = "fuzzy", seed = 42, k = 4)
fcns1 = tsclust(data_ns_1, type = "fuzzy", seed = 42, k = 4)
fcnf1 = tsclust(data_nf_1, type = "fuzzy", seed = 42, k = 4)

fcs2 = tsclust(data_s_2, type = "fuzzy", seed = 42, k = 4)
fcns2 = tsclust(data_ns_2, type = "fuzzy", seed = 42, k = 4)
fcnf2 = tsclust(data_nf_2, type = "fuzzy", seed = 42, k = 4)

fcs3 = tsclust(data_s_3, type = "fuzzy", seed = 42, k = 4)
fcns3 = tsclust(data_ns_3, type = "fuzzy", seed = 42, k = 4)
fcnf3 = tsclust(data_nf_3, type = "fuzzy", seed = 42, k = 4)

fcs4 = tsclust(data_s_4, type = "fuzzy", seed = 42, k = 4)
fcns4 = tsclust(data_ns_4, type = "fuzzy", seed = 42, k = 4)
fcnf4 = tsclust(data_nf_4, type = "fuzzy", seed = 42, k = 4)

fcs5 = tsclust(data_s_5, type = "fuzzy", seed = 42, k = 4)
fcns5 = tsclust(data_ns_5, type = "fuzzy", seed = 42, k = 4)
fcnf5 = tsclust(data_nf_5, type = "fuzzy", seed = 42, k = 4)

fcs6 = tsclust(data_s_6, type = "fuzzy", seed = 42, k = 4)
fcns6 = tsclust(data_ns_6, type = "fuzzy", seed = 42, k = 4)
fcnf6 = tsclust(data_nf_6, type = "fuzzy", seed = 42, k = 4)

fcs7 = tsclust(data_s_7, type = "fuzzy", seed = 42, k = 4)
fcns7 = tsclust(data_ns_7, type = "fuzzy", seed = 42, k = 4)
fcnf7 = tsclust(data_nf_7, type = "fuzzy", seed = 42, k = 4)

fcs8 = tsclust(data_s_8, type = "fuzzy", seed = 42, k = 4)
fcns8 = tsclust(data_ns_8, type = "fuzzy", seed = 42, k = 4)
fcnf8 = tsclust(data_nf_8, type = "fuzzy", seed = 42, k = 4)

fcs9 = tsclust(data_s_9, type = "fuzzy", seed = 42, k = 4)
fcns9 = tsclust(data_ns_9, type = "fuzzy", seed = 42, k = 4)
fcnf9 = tsclust(data_nf_9, type = "fuzzy", seed = 42, k = 4)

fcs10 = tsclust(data_s_10, type = "fuzzy", seed = 42, k = 4)
fcns10 = tsclust(data_ns_10, type = "fuzzy", seed = 42, k = 4)
fcnf10 = tsclust(data_nf_10, type = "fuzzy", seed = 42, k = 4)

fcs11 = tsclust(data_s_11, type = "fuzzy", seed = 42, k = 4)
fcns11 = tsclust(data_ns_11, type = "fuzzy", seed = 42, k = 4)
fcnf11 = tsclust(data_nf_11, type = "fuzzy", seed = 42, k = 4)

centroids1 = append(append(fcs1@centroids, fcns1@centroids), fcnf1@centroids)
centroids2 = append(append(fcs2@centroids, fcns2@centroids), fcnf2@centroids)
centroids3 = append(append(fcs3@centroids, fcns3@centroids), fcnf3@centroids)
centroids4 = append(append(fcs4@centroids, fcns4@centroids), fcnf4@centroids)
centroids5 = append(append(fcs5@centroids, fcns5@centroids), fcnf5@centroids)
centroids6 = append(append(fcs6@centroids, fcns6@centroids), fcnf6@centroids)
centroids7 = append(append(fcs7@centroids, fcns7@centroids), fcnf7@centroids) 
centroids8 = append(append(fcs8@centroids, fcns8@centroids), fcnf8@centroids)
centroids9 = append(append(fcs9@centroids, fcns9@centroids), fcnf9@centroids)
centroids10 = append(append(fcs10@centroids, fcns10@centroids), fcnf10@centroids) 
centroids11 = append(append(fcs11@centroids, fcns11@centroids), fcnf11@centroids)

data_1 = append(append(data_s_1, data_ns_1), data_nf_1)
data_2 = append(append(data_s_2, data_ns_2), data_nf_2)
data_3 = append(append(data_s_3, data_ns_3), data_nf_3)
data_4 = append(append(data_s_4, data_ns_4), data_nf_4)
data_5 = append(append(data_s_5, data_ns_5), data_nf_5)
data_6 = append(append(data_s_6, data_ns_6), data_nf_6)
data_7 = append(append(data_s_7, data_ns_7), data_nf_7)
data_8 = append(append(data_s_8, data_ns_8), data_nf_8)
data_9 = append(append(data_s_9, data_ns_9), data_nf_9)
data_10 = append(append(data_s_10, data_ns_10), data_nf_10)
data_11 = append(append(data_s_11, data_ns_11), data_nf_11)

clusters_1 = as.integer(c(fcs1@cluster, fcns1@cluster, fcnf1@cluster + 4))
clusters_2 = as.integer(c(fcs2@cluster, fcns2@cluster, fcnf2@cluster + 4))
clusters_3 = as.integer(c(fcs3@cluster, fcns3@cluster, fcnf3@cluster + 4))
clusters_4 = as.integer(c(fcs4@cluster, fcns4@cluster, fcnf4@cluster + 4))
clusters_5 = as.integer(c(fcs5@cluster, fcns5@cluster, fcnf5@cluster + 4))
clusters_6 = as.integer(c(fcs6@cluster, fcns6@cluster, fcnf6@cluster + 4))
clusters_7 = as.integer(c(fcs7@cluster, fcns7@cluster, fcnf7@cluster + 4))
clusters_8 = as.integer(c(fcs8@cluster, fcns8@cluster, fcnf8@cluster + 4))
clusters_9 = as.integer(c(fcs9@cluster, fcns9@cluster, fcnf9@cluster + 4))
clusters_10 = as.integer(c(fcs10@cluster, fcns10@cluster, fcnf10@cluster + 4))
clusters_11 = as.integer(c(fcs11@cluster, fcns11@cluster, fcnf11@cluster + 4))

fcm_1 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_1, centroids = centroids1, cluster = clusters_1, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_2 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_2, centroids = centroids2, cluster = clusters_2, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_3 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_3, centroids = centroids3, cluster = clusters_3, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_4 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_4, centroids = centroids4, cluster = clusters_4, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_5 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_5, centroids = centroids5, cluster = clusters_5, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_6 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_6, centroids = centroids6, cluster = clusters_6, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_7 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_7, centroids = centroids7, cluster = clusters_7, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_8 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_8, centroids = centroids8, cluster = clusters_8, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_9 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_9, centroids = centroids9, cluster = clusters_9, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_10 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_10, centroids = centroids10, cluster = clusters_10, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())
fcm_11 = new("FuzzyTSClusters", type = "fuzzy", datalist = data_11, centroids = centroids11, cluster = clusters_11, distance = "dtw_basic", centroid = "fcm", control = fuzzy_control())

r_s_1 = rep("0", length(data_s_1))
r_ns_1 = rep("1", length(data_ns_1))
r_nf_1 = rep("2", length(data_nf_1))

r_s_2 = rep("0", length(data_s_2))
r_ns_2 = rep("1", length(data_ns_2))
r_nf_2 = rep("2", length(data_nf_2))

r_s_3 = rep("0", length(data_s_3))
r_ns_3 = rep("1", length(data_ns_3))
r_nf_3 = rep("2", length(data_nf_3))

r_s_4 = rep("0", length(data_s_4))
r_ns_4 = rep("1", length(data_ns_4))
r_nf_4 = rep("2", length(data_nf_4))

r_s_5 = rep("0", length(data_s_5))
r_ns_5 = rep("1", length(data_ns_5))
r_nf_5 = rep("2", length(data_nf_5))

r_s_6 = rep("0", length(data_s_6))
r_ns_6 = rep("1", length(data_ns_6))
r_nf_6 = rep("2", length(data_nf_6))

r_s_7 = rep("0", length(data_s_7))
r_ns_7 = rep("1", length(data_ns_7))
r_nf_7 = rep("2", length(data_nf_7))

r_s_8 = rep("0", length(data_s_8))
r_ns_8 = rep("1", length(data_ns_8))
r_nf_8 = rep("2", length(data_nf_8))

r_s_9 = rep("0", length(data_s_9))
r_ns_9 = rep("1", length(data_ns_9))
r_nf_9 = rep("2", length(data_nf_9))

r_s_10 = rep("0", length(data_s_10))
r_ns_10 = rep("1", length(data_ns_10))
r_nf_10 = rep("2", length(data_nf_10))

r_s_11 = rep("0", length(data_s_11))
r_ns_11 = rep("1", length(data_ns_11))
r_nf_11 = rep("2", length(data_nf_11))

treatment = c(r_s_1, r_ns_1, r_nf_1, r_s_2, r_ns_2, r_nf_2, r_s_3, r_ns_3, r_nf_3, r_s_4, r_ns_4, r_nf_4, r_s_5, r_ns_5, r_nf_5, r_s_6, r_ns_6, r_nf_6, r_s_7, r_ns_7, r_nf_7, r_s_8, r_ns_8, r_nf_8, r_s_9, r_ns_9, r_nf_9, r_s_10, r_ns_10, r_nf_10, r_s_11, r_ns_11, r_nf_11)
y_train = factor(treatment, c("0", "1", "2"))
x_train = cbind(fcm_1@fcluster, fcm_2@fcluster, fcm_3@fcluster, fcm_4@fcluster, fcm_5@fcluster, fcm_6@fcluster, fcm_7@fcluster, fcm_8@fcluster, fcm_9@fcluster, fcm_10@fcluster, fcm_11@fcluster)
model = svm(x_train, y_train)

l <- list(fcm_1, fcm_2, fcm_3, fcm_4, fcm_5, fcm_6, fcm_7, fcm_8, fcm_9, fcm_10, fcm_11, model)
dataframe_container <- data.frame(out2 = as.integer(serialize(l, connection=NULL)))

write.csv(dataframe_container, "C:/Users/Артем Жиленков/Desktop/Магистрский IT/Diploma/подсистема Идентификации возникновения АР/classifier.csv")