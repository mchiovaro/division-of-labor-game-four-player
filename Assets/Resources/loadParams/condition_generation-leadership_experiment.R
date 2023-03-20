##### Randomizing conditions for leadership experiment #####
#
# This script creates the randomized condition orders for 
# the task allocation experiments.
#
# Created by: M. Chiovaro (@mchiovaro)
# Last updated: 2023_03_20

##### Set up #####

# set working directory
setwd("./Assets/Resources/loadParams/")

# set seed for reproducibility
set.seed(2023)

# create a new data frame with condition columns
df <- data.frame(matrix(ncol = 17, nrow = 45))

# provide column names
colnames(df) <- c('group_number', 'com_condition',
                  'td_1', 'td_2', 'td_3', 'td_4', 
                  'td_5', 'td_6', 
                  'ie_1', 'ie_2', 'ie_3', 'ie_4', 
                  'ie_5', 'ie_6', 
                  'leader_cond', 'practice', 'experiment_number')

##### Experiment number #####
df$experiment_number[1:45] <- 4

##### Task demand conditions #####

# for the practice round
shuffled_practice <- sample(1:3, size = nrow(df), replace = TRUE)
df$practice <- shuffled_practice

# for all rows - create the random task condition
for (i in 1:nrow(df)){
  
  # create a random sequence of 1 through 6 
  shuffled <- sample(1:6, size = 6, replace = FALSE)
  
  # turn it into a data frame
  sample <- t(as.data.frame(shuffled))
  
  # fill it into a row
  df[i, c(3:8)] <- sample
  
  # for each column in that row
  for (j in 3:8){
    
    # if the value is greater than 3
    if(df[i, j] > 3){
      
      # subtract 3 (making 4 -> 1, 5 -> 2, 6 -> 3)
      df[i, j] <- df[i, j] - 3 
      
    }
    
  }
  
}

##### Individual effectivities condition #####

# for all rounds of this experiment, allow them to both switch
df$practice_ie[1:45] <- 3

# for all rows, create add ie as 3
for (i in 1:nrow(df)){
  for (j in 9:14){
    df[i,j] <- 3
  }
}

##### Communication condition #####
df$com_condition[1:45] <- 3

##### Leadership condition#####
df$leader_cond[1:15] <- 1
df$leader_cond[16:30] <- 2
df$leader_cond[31:45] <- 3

##### Shuffling rows #####
shuffled_data = df[sample(1:45), ]

##### Group number #####
shuffled_data$group_number <- seq.int(nrow(shuffled_data))

# write shuffled data to file
write.table(x = shuffled_data,
            file='./conditions-leadership.csv',
            sep=",",
            col.names=TRUE,
            row.names=FALSE)
