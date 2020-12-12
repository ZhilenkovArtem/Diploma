args <- commandArgs(trailingOnly=TRUE)

library(dtwclust)
library(e1071)
library(readr)

dataframe_container <- read_csv("C:/Users/Артем Жиленков/Desktop/Магистрский IT/Diploma/подсистема Идентификации возникновения АР/classifier.csv")
unser_obj <- unserialize(as.raw(dataframe_container$out2))
fcm_1 <- unser_obj[1][[1]]
fcm_2 <- unser_obj[2][[1]]
fcm_3 <- unser_obj[3][[1]]
fcm_4 <- unser_obj[4][[1]]
fcm_5 <- unser_obj[5][[1]]
fcm_6 <- unser_obj[6][[1]]
fcm_7 <- unser_obj[7][[1]]
fcm_8 <- unser_obj[8][[1]]
fcm_9 <- unser_obj[9][[1]]
fcm_10 <- unser_obj[10][[1]]
fcm_11 <- unser_obj[11][[1]]
model <- unser_obj[12][[1]]

Delta_1 <- strsplit(args[1], split = ",", fixed=TRUE)
Delta_2 <- strsplit(args[3], split = ",", fixed=TRUE)
Delta_3 <- strsplit(args[5], split = ",", fixed=TRUE)
Delta_4 <- strsplit(args[7], split = ",", fixed=TRUE)
Delta_5 <- strsplit(args[9], split = ",", fixed=TRUE)
Delta_6 <- strsplit(args[11], split = ",", fixed=TRUE)
Delta_7 <- strsplit(args[13], split = ",", fixed=TRUE)
Delta_8 <- strsplit(args[15], split = ",", fixed=TRUE)
Delta_9 <- strsplit(args[17], split = ",", fixed=TRUE)
Delta_10 <- strsplit(args[19], split = ",", fixed=TRUE)
Delta_11 <- strsplit(args[21], split = ",", fixed=TRUE)

U_1 <- strsplit(args[2], split = ",", fixed=TRUE)
U_2 <- strsplit(args[4], split = ",", fixed=TRUE)
U_3 <- strsplit(args[6], split = ",", fixed=TRUE)
U_4 <- strsplit(args[8], split = ",", fixed=TRUE)
U_5 <- strsplit(args[10], split = ",", fixed=TRUE)
U_6 <- strsplit(args[12], split = ",", fixed=TRUE)
U_7 <- strsplit(args[14], split = ",", fixed=TRUE)
U_8 <- strsplit(args[16], split = ",", fixed=TRUE)
U_9 <- strsplit(args[18], split = ",", fixed=TRUE)
U_10 <- strsplit(args[20], split = ",", fixed=TRUE)
U_11 <- strsplit(args[22], split = ",", fixed=TRUE)

V1 <- as(Delta_1[[1]][1], "numeric")
data_Delta_1 = data.frame(V1)
i = 2
while (i < length(Delta_1[[1]]) + 1){
V <- as(Delta_1[[1]][i], "numeric")
data_Delta_1[i] <- (V)
i = i + 1
}
V1 <- as(Delta_2[[1]][1], "numeric")
data_Delta_2 = data.frame(V1)
i = 2
while (i < length(Delta_2[[1]]) + 1){
V <- as(Delta_2[[1]][i], "numeric")
data_Delta_2[i] <- (V)
i = i + 1
}
V1 <- as(Delta_3[[1]][1], "numeric")
data_Delta_3 = data.frame(V1)
i = 2
while (i < length(Delta_3[[1]]) + 1){
V <- as(Delta_3[[1]][i], "numeric")
data_Delta_3[i] <- (V)
i = i + 1
}
V1 <- as(Delta_4[[1]][1], "numeric")
data_Delta_4 = data.frame(V1)
i = 2
while (i < length(Delta_4[[1]]) + 1){
V <- as(Delta_4[[1]][i], "numeric")
data_Delta_4[i] <- (V)
i = i + 1
}
V1 <- as(Delta_5[[1]][1], "numeric")
data_Delta_5 = data.frame(V1)
i = 2
while (i < length(Delta_5[[1]]) + 1){
V <- as(Delta_5[[1]][i], "numeric")
data_Delta_5[i] <- (V)
i = i + 1
}
V1 <- as(Delta_6[[1]][1], "numeric")
data_Delta_6 = data.frame(V1)
i = 2
while (i < length(Delta_6[[1]]) + 1){
V <- as(Delta_6[[1]][i], "numeric")
data_Delta_6[i] <- (V)
i = i + 1
}
V1 <- as(Delta_7[[1]][1], "numeric")
data_Delta_7 = data.frame(V1)
i = 2
while (i < length(Delta_7[[1]]) + 1){
V <- as(Delta_7[[1]][i], "numeric")
data_Delta_7[i] <- (V)
i = i + 1
}
V1 <- as(Delta_8[[1]][1], "numeric")
data_Delta_8 = data.frame(V1)
i = 2
while (i < length(Delta_8[[1]]) + 1){
V <- as(Delta_8[[1]][i], "numeric")
data_Delta_8[i] <- (V)
i = i + 1
}
V1 <- as(Delta_9[[1]][1], "numeric")
data_Delta_9 = data.frame(V1)
i = 2
while (i < length(Delta_9[[1]]) + 1){
V <- as(Delta_9[[1]][i], "numeric")
data_Delta_9[i] <- (V)
i = i + 1
}
V1 <- as(Delta_10[[1]][1], "numeric")
data_Delta_10 = data.frame(V1)
i = 2
while (i < length(Delta_10[[1]]) + 1){
V <- as(Delta_10[[1]][i], "numeric")
data_Delta_10[i] <- (V)
i = i + 1
}
V1 <- as(Delta_11[[1]][1], "numeric")
data_Delta_11 = data.frame(V1)
i = 2
while (i < length(Delta_11[[1]]) + 1){
V <- as(Delta_11[[1]][i], "numeric")
data_Delta_11[i] <- (V)
i = i + 1
}

V1 <- as(U_1[[1]][1], "numeric")
data_U_1 = data.frame(V1)
i = 2
while (i < length(U_1[[1]]) + 1){
V <- as(U_1[[1]][i], "numeric")
data_U_1[i] <- (V)
i = i + 1
}
V1 <- as(U_2[[1]][1], "numeric")
data_U_2 = data.frame(V1)
i = 2
while (i < length(U_2[[1]]) + 1){
V <- as(U_2[[1]][i], "numeric")
data_U_2[i] <- (V)
i = i + 1
}
V1 <- as(U_3[[1]][1], "numeric")
data_U_3 = data.frame(V1)
i = 2
while (i < length(U_3[[1]]) + 1){
V <- as(U_3[[1]][i], "numeric")
data_U_3[i] <- (V)
i = i + 1
}
V1 <- as(U_4[[1]][1], "numeric")
data_U_4 = data.frame(V1)
i = 2
while (i < length(U_4[[1]]) + 1){
V <- as(U_4[[1]][i], "numeric")
data_U_4[i] <- (V)
i = i + 1
}
V1 <- as(U_5[[1]][1], "numeric")
data_U_5 = data.frame(V1)
i = 2
while (i < length(U_5[[1]]) + 1){
V <- as(U_5[[1]][i], "numeric")
data_U_5[i] <- (V)
i = i + 1
}
V1 <- as(U_6[[1]][1], "numeric")
data_U_6 = data.frame(V1)
i = 2
while (i < length(U_6[[1]]) + 1){
V <- as(U_6[[1]][i], "numeric")
data_U_6[i] <- (V)
i = i + 1
}
V1 <- as(U_7[[1]][1], "numeric")
data_U_7 = data.frame(V1)
i = 2
while (i < length(U_7[[1]]) + 1){
V <- as(U_7[[1]][i], "numeric")
data_U_7[i] <- (V)
i = i + 1
}
V1 <- as(U_8[[1]][1], "numeric")
data_U_8 = data.frame(V1)
i = 2
while (i < length(U_8[[1]]) + 1){
V <- as(U_8[[1]][i], "numeric")
data_U_8[i] <- (V)
i = i + 1
}
V1 <- as(U_9[[1]][1], "numeric")
data_U_9 = data.frame(V1)
i = 2
while (i < length(U_9[[1]]) + 1){
V <- as(U_9[[1]][i], "numeric")
data_U_9[i] <- (V)
i = i + 1
}
V1 <- as(U_10[[1]][1], "numeric")
data_U_10 = data.frame(V1)
i = 2
while (i < length(U_10[[1]]) + 1){
V <- as(U_10[[1]][i], "numeric")
data_U_10[i] <- (V)
i = i + 1
}
V1 <- as(U_11[[1]][1], "numeric")
data_U_11 = data.frame(V1)
i = 2
while (i < length(U_11[[1]]) + 1){
V <- as(U_11[[1]][i], "numeric")
data_U_11[i] <- (V)
i = i + 1
}

data_1 = vector("list", nrow(data_U_1))
i = 1
while (i < nrow(data_U_1) + 1){
data_1[[i]] = cbind(as.numeric(data_U_1[i,]), as.numeric(data_Delta_1[i,]))
i = i + 1
}
data_2 = vector("list", nrow(data_U_2))
i = 1
while (i < nrow(data_U_2) + 1){
data_2[[i]] = cbind(as.numeric(data_U_2[i,]), as.numeric(data_Delta_2[i,]))
i = i + 1
}
data_3 = vector("list", nrow(data_U_3))
i = 1
while (i < nrow(data_U_3) + 1){
data_3[[i]] = cbind(as.numeric(data_U_3[i,]), as.numeric(data_Delta_3[i,]))
i = i + 1
}
data_4 = vector("list", nrow(data_U_4))
i = 1
while (i < nrow(data_U_4) + 1){
data_4[[i]] = cbind(as.numeric(data_U_4[i,]), as.numeric(data_Delta_4[i,]))
i = i + 1
}
data_5 = vector("list", nrow(data_U_5))
i = 1
while (i < nrow(data_U_5) + 1){
data_5[[i]] = cbind(as.numeric(data_U_5[i,]), as.numeric(data_Delta_5[i,]))
i = i + 1
}
data_6 = vector("list", nrow(data_U_6))
i = 1
while (i < nrow(data_U_6) + 1){
data_6[[i]] = cbind(as.numeric(data_U_6[i,]), as.numeric(data_Delta_6[i,]))
i = i + 1
}
data_7 = vector("list", nrow(data_U_7))
i = 1
while (i < nrow(data_U_7) + 1){
data_7[[i]] = cbind(as.numeric(data_U_7[i,]), as.numeric(data_Delta_7[i,]))
i = i + 1
}
data_8 = vector("list", nrow(data_U_8))
i = 1
while (i < nrow(data_U_8) + 1){
data_8[[i]] = cbind(as.numeric(data_U_8[i,]), as.numeric(data_Delta_8[i,]))
i = i + 1
}
data_9 = vector("list", nrow(data_U_9))
i = 1
while (i < nrow(data_U_9) + 1){
data_9[[i]] = cbind(as.numeric(data_U_9[i,]), as.numeric(data_Delta_9[i,]))
i = i + 1
}
data_10 = vector("list", nrow(data_U_10))
i = 1
while (i < nrow(data_U_10) + 1){
data_10[[i]] = cbind(as.numeric(data_U_10[i,]), as.numeric(data_Delta_10[i,]))
i = i + 1
}
data_11 = vector("list", nrow(data_U_11))
i = 1
while (i < nrow(data_U_11) + 1){
data_11[[i]] = cbind(as.numeric(data_U_11[i,]), as.numeric(data_Delta_11[i,]))
i = i + 1
}
matr_1 = predict(fcm_1, data_1)
matr_2 = predict(fcm_2, data_2)
matr_3 = predict(fcm_3, data_3)
matr_4 = predict(fcm_4, data_4)
matr_5 = predict(fcm_5, data_5)
matr_6 = predict(fcm_6, data_6)
matr_7 = predict(fcm_7, data_7)
matr_8 = predict(fcm_8, data_8)
matr_9 = predict(fcm_9, data_9)
matr_10 = predict(fcm_10, data_10)
matr_11 = predict(fcm_11, data_11)

x_test = cbind(matr_1, matr_2, matr_3, matr_4, matr_5, matr_6, matr_7, matr_8, matr_9, matr_10, matr_11)
pred_test = predict(model, x_test)
pred_test