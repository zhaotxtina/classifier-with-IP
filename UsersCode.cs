using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Numerics;
using PGL.IP.API;
using PGL.IP.Services;
using PGL.IP.Utils;
using Accord;
using Accord.Math;
using Accord.IO;
using Accord.MachineLearning.Bayes;
using Accord.Controls;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using PGL.IP.UserProgDotNetInterface;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics;
using Accord.Math.Optimization.Losses;
using Accord.MachineLearning;
using Accord.Statistics.Analysis;
using System.Windows.Forms;
using Accord.MachineLearning.Performance;

public partial class IPLink 
{

    public void UserCode()
    {
        List<float> gr = new List<float>();
        List<float> rdeep = new List<float>();
        List<float> depth = new List<float>();
        List<int> rhobBadFlag = new List<int>();
        //var api=new IntPetroAPI();  //get api instance
        //IMessageBoard messageBoard = api.GetService<IMessageBoard>();
        StringBuilder message = new StringBuilder();
        var data = new DataTable();
        var datatest = new DataTable();
        data.Columns.Add("inde", "depth", "gr", "rdeep", "bhflag");
        datatest.Columns.Add("depth","testin1", "testin2");

        int index;
        //
        // Loop through the data one level at a time
        // TopDepth and BottomDepth are the index equivalent depths entered on the run window. 
        // finding the max for the curves gr and rdeep, for normalization use 
        // for test curve 1 and 2 too, they are used for applying the classification model
        Single grmax, rdeepmax, testin1max, testin2max;
        grmax = -0.0000001f;
        rdeepmax = -0.0000001f;
        testin1max= -0.0000001f;
        testin2max = -0.0000001f;
        for (index = TopDepth; index <= BottomDepth; index += 1)
        {
            if ((GR(index) > grmax) && (GR(index) != -999.0))
            {
                grmax = GR(index);
            }

            if ((RDEEP(index) > rdeepmax) && (RDEEP(index) != -999.0))
            {
                rdeepmax = RDEEP(index);
            }

            if ((testin1(index) > testin1max) && (testin1(index) != -999.0))
            {
                testin1max = testin1(index);
            }

            if ((testin2(index) > testin2max) && (testin2(index) != -999.0))
            {
                testin2max = testin2(index);
            }
        }

        // read in the curves and saved in a data table, to be used in model building

        for (index = TopDepth; index <= BottomDepth; index += 1)
        {
            // Enter user code here 

            if ((GR(index) != -999) && (RDEEP(index) != -999))
            {
                //gr.Add(GR(index));   // for saving the list as a file
                //rdeep.Add(RDEEP(index));
                //depth.Add(Depth(index));
                // in the code generate the labels used for classification        
                int bhflag = 1;
                if ( (GR(index)/grmax < 0.5) && (RDEEP(index)/rdeepmax<0.5))
                //if ((RDEEP(index)/rdeepmax < 0.5))
                {
                    bhflag = 0;
                }
                //rhobBadFlag.Add(bhflag);
                data.Rows.Add(index, Depth(index), GR(index) / grmax, RDEEP(index) / rdeepmax, bhflag);
                //message.AppendFormat("Depth: {0}\t Gamma Value: {1:0.00}\t resistivity Value:{2:0.00} \n", Depth(index), GR(index), RDEEP(index));
                //message.AppendFormat("Depth: {0}\t Gamma Value: {1:0.00}\t resistivity Value:{2:0.00}\t flag:{3:0.00} \n", Depth(index), GR(index), RDEEP(index), bhflag);
            }
            // keep the NA values but assign a different class to it, this is of no interest, but need to keep to write back when saving it as a curve later
            else
            {
                data.Rows.Add(index, Depth(index), -999 / 500, -999 / 500, 2);
            }
            // read in the test curves too
            if ((testin1(index) != -999) && (testin2(index) != -999))
            {
                
                datatest.Rows.Add( Depth(index), testin1(index) / testin1max, testin2(index) / testin2max);
                     }
            // keep the NA values but assign a different class to it, this is of no interest, but need to keep to write back when saving it as a curve later
            else
            {
                datatest.Rows.Add(Depth(index), -999 / 500, -999 / 500);
            }



        }
        

        //MessageBox.Show(message.ToString());
        //File.WriteAllLines("mygr.txt", gr.Select(x => x.ToString()).ToArray());
        //File.WriteAllLines("myrdeep.txt", rdeep.Select(x => x.ToString()).ToArray());
        //File.WriteAllLines("mydepth.txt", depth.Select(x => x.ToString()).ToArray());
        //File.WriteAllLines("mybadflag.txt", rhobBadFlag.Select(x => x.ToString()).ToArray());

        //

        double[][] inputs = data.ToJagged<double>("gr", "rdeep");
        int[] outputs = data.Columns["bhflag"].ToArray<int>();

        double[][] inputtest = datatest.ToJagged<double>("testin1", "testin2");
        //int[] outputs = data.Columns["bhflag"].ToArray<int>();

        //int[] indices = data.Columns["inde"].ToArray<int>();
        //ScatterplotBox.Show("rhobbadflag vs gr and rhob training data", inputs, outputs).Hold();
        #region gridsearch crossvalidation to find best parameters
        //var gscv = GridSearch.CrossValidate(

        // // Here we can specify the range of the parameters to be included in the search
        //ranges: new
        //{
        //Join = GridSearch.Range(fromInclusive: 1, toExclusive: 20),
        //MaxHeight = GridSearch.Range(fromInclusive: 1, toExclusive: 20),
        //},

        //  // Indicate how learning algorithms for the models should be created
        //learner: (p, ss) => new C45Learning
        //{
        //// Here, we can use the parameters we have specified above:
        //    Join = p.Join,
        //    MaxHeight = p.MaxHeight,
        // },

        //// Define how the model should be learned, if needed
        //fit: (teacher, x, y, w) => teacher.Learn(x, y, w),

        //// Define how the performance of the models should be measured
        //loss: (actual, expected, r) => new ZeroOneLoss(expected).Loss(actual),

        //folds: 3, // use k = 3 in k-fold cross validation

        //x: inputs, y: outputs // so the compiler can infer generic types
        //);

        //// If needed, control the parallelization degree
        //gscv.ParallelOptions.MaxDegreeOfParallelism = 1;

        //// Search for the best decision tree
        //var result = gscv.Learn(inputs, outputs);

        //// Get the best cross-validation result:
        //var crossValidation = result.BestModel;

        //// Get an estimate of its error:
        //double bestAverageError = result.BestModelError;

        //double trainError = result.BestModel.Training.Mean;
        //double trainErrorVar = result.BestModel.Training.Variance;
        //double valError = result.BestModel.Validation.Mean;
        //double valErrorVar = result.BestModel.Validation.Variance;

        //// Get the best values for the parameters:
        //int bestJoin = result.BestParameters.Join;
        //int bestHeight = result.BestParameters.MaxHeight;

        //// Use the best parameter values to create the final 
        //// model using all the training and validation data:
        //var bestTeacher = new C45Learning
        //{
        //    Join = bestJoin,
        //    MaxHeight = bestHeight,
        //};

        //// Use the best parameters to create the final tree model:
        //DecisionTree finalTree = bestTeacher.Learn(inputs, outputs);
        //
        #endregion
        //// grid search takes too long, suggest to do it separately, not in production time
        // c45 decision tree  default parameter without tuning, sometimes give the wrong classification
        //C45Learning Teacher = new C45Learning(new[] {

        //    DecisionVariable.Continuous("gr"),
        //    DecisionVariable.Continuous("rdeep")
        //   });
        var Teacher = new C45Learning
        {
            Join = 2,
            MaxHeight = 5,
        };

        // Use the learning algorithm to induce the tree
        DecisionTree tree = Teacher.Learn(inputs, outputs);
        // Compute the error in the learning
        double error = new ZeroOneLoss(outputs).Loss(tree.Decide(inputs));
        // Classify the samples using the model
        
        int[] answers3 = tree.Decide(inputs);

                ////// Plot the results
        ScatterplotBox.Show("original training data", inputs, outputs);
        ScatterplotBox.Show("DecisionTree modeling training data", inputs, answers3);

        //output the confusionmatrix matrics
        var cm = new GeneralConfusionMatrix(classes: 3, expected: outputs, predicted: answers3);

        //Console.WriteLine("training accuracy is");
        //Console.WriteLine(cm.Accuracy);
        //Console.WriteLine("training error is");
        //Console.WriteLine(cm.Error);
        message.AppendFormat("Classification Training Performance: \t Accuracy: {0}\t Error:{1:0.00} \n", cm.Accuracy, cm.Error);
        MessageBox.Show(message.ToString());


        int[] answers4 = tree.Decide(inputtest);

        ////// Plot the results
        
        ScatterplotBox.Show("DecisionTree modeling testing data", inputtest, answers4);

        
        #region save the classifier labels to curves
        int index3;
        //List<int> bhFlaglist = new List<int>(); 
        // save the original flag
        for (index3 = TopDepth; index3 <= BottomDepth; index3 += 1)  // loop through all depth index
        {
            // save NA values to curve
            if (outputs[index3 - 1] == 2)
            {
                float inna = -999;
                Save_originalflag(index3, inna);
            }
            else // save the real values to curve
            {
                float inna = outputs[index3 - 1];
                Save_originalflag(index3, inna);
            }

        }


        //save the classifier flag generated by the model 
        int index2;
        for (index2 = TopDepth; index2 <= BottomDepth; index2 += 1)  // loop through all depth index
        {
            // save NA values to curve
            if (answers3[index2 - 1] == 2)
            {
                float inna1 = -999;
                Save_classflag(index2, inna1);

            }
            else    // save the real values to curve
            {
                float inna1 = answers3[index2 - 1];
                Save_classflag(index2, inna1);

            }

        }

        //save the classifier flag generated by the model 
        int index4;
        for (index4 = TopDepth; index4 <= BottomDepth; index4 += 1)  // loop through all depth index
        {
            // save NA values to curve
            if (answers4[index4 - 1] == 2)
            {
                float inna2 = -999;
                Save_testflag(index4, inna2);

            }
            else    // save the real values to curve
            {
                float inna2 = answers4[index4 - 1];
                Save_testflag(index4, inna2);

            }

        }

        #endregion

        //// no real meaning here, just to put a breakpoint here for debug purpose
        //int indexend = data.Rows.Count;

    }

}

