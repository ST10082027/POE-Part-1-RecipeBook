using System;
namespace RecipeBook
{
    /*-------
    Class
    -------
    This class contains the variables and methods for the Recipe Book. It carries out the main operation of the application
    */
    public class RecipeDetails
    {
        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Variables
        These are used to store information regarding the recipe.*/
        private int numIngredients;
        private string[]? ingredientNames;
        private double[]? ingredientQuantities;
        private double[]? originalQuant;
        private string[]? ingredientUnits;
        private int numSteps;
        private string[]? stepDescriptions;
        private double scaleFactor =1;

        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method prompts the user for details then captures their input.
        */
        public void EnterRecipe()
        {
            //Prompt for number of ingredients, error handling and variable capture
            Console.Write("Enter the number of ingredients: ");
            if (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients < 1)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
                return;
            }
           
            //Variables
            //Used to insert the user input into the appropriate arrays
            ingredientNames = new string[numIngredients];
            ingredientQuantities = new double[numIngredients];
            ingredientUnits = new string[numIngredients];

            /*Loop
            For Loop to iterate through the total number of ingredients. In this loop the details of each ingredient
            is captured.*/
            for (int i = 0; i < numIngredients; i++)
            {
                //Prompt for the name of ingredient
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                ingredientNames[i] = Console.ReadLine();

                //Prompt for the quantity of ingredient and error handling.
                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out ingredientQuantities[i]) || ingredientQuantities[i] < 0)
                {
                    Console.WriteLine($"Invalid input for quantity of ingredient {i + 1}. Please enter a number.");
                    i--;
                    continue;
                }

                //Prompt for the measurement unit of the ingredient
                Console.Write($"Enter the unit of measurement for ingredient {i + 1}: ");
                ingredientUnits[i] = Console.ReadLine();
            }

            //Prompt for the amount of steps in the recipe and error handling
            Console.Write("Enter the number of steps: ");
            if (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps < 1)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            /*Loop
            For Loop to iterates through the total number of steps for the recipe. In this loop the description of each
            step is captured and numbered.*/
            stepDescriptions = new string[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");
                stepDescriptions[i] = Console.ReadLine();
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method displays the recipe after it has been captured. The method also displays the scale recipe if the user
        chooses to do so.
        */
        public void DisplayRecipe()
        {
            //If statement checks whether or not a recipe has been captured by the user. If not an error message is displayed
            if (numIngredients == 0 && numSteps == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("==========================================");
            Console.WriteLine("Ingredients:");
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine(" {0} {1} {2}", ingredientQuantities[i]*scaleFactor, ingredientUnits[i], ingredientNames[i]);
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine("{0} {1}", i + 1, stepDescriptions[i]);
            }
            Console.WriteLine("==========================================\n");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        This method scales the recipe's portion up or down by a predetermined margin. This is achieved by mutliplying the
        number of ingredients with 0.5, 2, 3 or a value determined by the user.
        */
        public void ScaleRecipe()
        {
            //Stores the value by which the number of ingredients will be multiplied
            

            //If statement checks whether or not a recipe has been captured by the user 
            if (numIngredients == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            //Prompts
            Console.WriteLine("==========================================");
            Console.WriteLine("Select a scaling factor:");
            Console.WriteLine("1. 0.5x");
            Console.WriteLine("2. 2x");
            Console.WriteLine("3. 3x");
            Console.WriteLine("4. Custom");
            Console.WriteLine("==========================================\n");

            int choice = int.Parse(Console.ReadLine());

            /*Switch Case
             Multiplies the ingredients to scale the portion size of the recipe*/
            switch (choice)
            {
                case 1:
                    scaleFactor = 0.5;
                    break;

                case 2:
                    scaleFactor = 2;
                    break;

                case 3:
                    scaleFactor = 3;
                    break;

                case 4:
                    Console.WriteLine("Enter a custom scaling factor:");
                    scaleFactor = double.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            Console.WriteLine("\nScaled Recipe:");

            for (int i = 0; i < numIngredients; i++)
            {
                double scaledQuantity = ingredientQuantities[i] * scaleFactor;
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]} => {scaledQuantity} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Resets the quantities of the recipe and prompts the user to enter new quantities.
        */
        public void ResetQuantities()
        {
            if (numIngredients == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }

            Console.WriteLine("Enter new quantities for each ingredient:");
            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"New quantity for {ingredientNames[i]} ({ingredientUnits[i]}): ");
                string newQuantityString = Console.ReadLine();
                if (!double.TryParse(newQuantityString, out double newQuantity))
                {
                    Console.WriteLine($"Invalid quantity entered for {ingredientNames[i]} ({ingredientUnits[i]}). Quantity will remain unchanged.");
                }
                else
                {
                    ingredientQuantities[i] = newQuantity;
                }
            }

            Console.WriteLine("Recipe quantities have been reset.");
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Resets the entire recipe by nullifying all the information captured by the user
        */
        public void ClearRecipe()
        {
            if (numIngredients == 0 && numSteps == 0)
            {
                NoRecipeFoundErrorMessage();
                return;
            }
            numIngredients = 0;
            ingredientNames = null;
            ingredientQuantities = null;
            ingredientUnits = null;
            numSteps = 0;
            stepDescriptions = null;
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*
        |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Creates and displays a user interface.
        */
        public void UserInterface()
        {
            /*Obejct
             Instantiates the RecipeDetails class to access all of its contents
             */
            RecipeDetails recipe = new RecipeDetails();

            /*Loop
             While Loop*/
            while (true)
            {
                Console.WriteLine("==========================================");
                Console.WriteLine("Recipe Book");
                Console.WriteLine("==========================================");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display the recipe");
                Console.WriteLine("3. Scale the recipe");
                Console.WriteLine("4. Reset the quantities");
                Console.WriteLine("5. Clear the recipe");
                Console.WriteLine("6. Exit");
                Console.WriteLine("==========================================\n");

                string choiceString = Console.ReadLine();

                if (int.TryParse(choiceString, out int choice))
                {
                    /*Switch Case
                     Runs the method selected by the user*/
                    switch (choice)
                    {
                        case 1:
                            recipe.EnterRecipe();
                            break;

                        case 2:
                            recipe.DisplayRecipe();
                            break;

                        case 3:

                            recipe.ScaleRecipe();
                            break;

                        case 4:
                            recipe.ResetQuantities();
                            break;

                        case 5:
                            recipe.ClearRecipe();
                            break;

                        case 6:
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            recipe.InvalidInputErrorMessage();
                            break;
                    }
                }
                else
                {
                    recipe.InvalidInputErrorMessage();
                }
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Message notifying the user that they have not entered a recipe.*/
        public void NoRecipeFoundErrorMessage()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("!!! E R R O R !!!");
            Console.WriteLine("No recipe found. Please enter a recipe and try again.");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        /*|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        Method
        Message notifying of invalid input*/
        public void InvalidInputErrorMessage()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("!!! E R R O R !!!");
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();
        }
    }
}



