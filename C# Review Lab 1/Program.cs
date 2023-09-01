using System.Globalization;

namespace C__Review_Lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<VideoGame> games = new List<VideoGame>();
            

            //These two lines are directly from the amazing teacher Will, they help locate the root folder creating a
            //dynamic file path that doesn't need to be entered.
            string projectRootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string filePath = projectRootFolder + "/videogames.csv";

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] lineData = line.Split(',');

                    VideoGame game = new VideoGame(lineData[0], lineData[1], lineData[2], lineData[3], lineData[4], Convert.ToDouble(lineData[5]), Convert.ToDouble(lineData[6]), Convert.ToDouble(lineData[7]), Convert.ToDouble(lineData[8]), Convert.ToDouble(lineData[9]));

                    games.Add(game);
                }
            }

            games.Sort();

            /*
            //Displays all games alphabetically  
            foreach (var game in games)
            {
                Console.WriteLine(game);
            }
            */

            //Displays all games from the specific publisher 
            //List<VideoGame> publisherSpesific = PublisherSort("Activision", games);

            //foreach (var game in publisherSpesific)
            //{
            //    Console.WriteLine(game);
            //}
            //Console.WriteLine(ResponseGenerator(games, publisherSpesific, "Developed by","Activision",""));



            //Displays all games from the specific genere
            //List<VideoGame> genereSpesific = GenereSort("Role-Playing", games);

            //foreach (var game in genereSpesific)
            //{
            //    Console.WriteLine(game);
            //}
            //Console.WriteLine(ResponseGenerator(games, genereSpesific, "", "Role-Playing", "games"));

            //List<string> publisherList = PossibleAnswers(games);

            //foreach (string publisher in publisherList) 
            //{
            //    Console.WriteLine(publisher);
            //}
            //Console.WriteLine(publisherList.Count);
            PublisherData(games);
            Console.WriteLine("\n");
            GenereData(games);
        }



        static List<VideoGame> PublisherSort(string publisher, List<VideoGame> games)
        {
            List<VideoGame> publisherSpesific = new List<VideoGame>();
            var publisherGame =
                from VideoGame in games
                where VideoGame.Publisher == publisher
                select VideoGame;

            foreach (var item in publisherGame)
            {
                publisherSpesific.Add(item);
            }
            return publisherSpesific;
        }

        static List<VideoGame> GenereSort(string genere, List<VideoGame> games)
        {
            List<VideoGame> genereSpesific = new List<VideoGame>();
            var genereGame = 
                from VideoGame in games
                where VideoGame.Genere == genere
                select VideoGame;

            foreach (var item in genereGame)
            {
                genereSpesific.Add(item);
            }
            return genereSpesific;
        }

        static double PercentFinder(List<VideoGame> allGame, List<VideoGame> specificGame)
        {
            double chosenPercent = Convert.ToDouble(specificGame.Count()) / Convert.ToDouble(allGame.Count());
            return Math.Round((chosenPercent)*100,2);
        }

        static string ResponseGenerator(List<VideoGame> allGame, List<VideoGame> specificGame, string specificationBefore, string sortType, string specificationAfter)
        {
            string msg;
            msg = $"Out of {allGame.Count} games, {specificGame.Count} are {specificationBefore} {sortType} {specificationAfter}, which is {PercentFinder(allGame,specificGame)}%";
            return msg;
        }


        private static void PublisherData(List<VideoGame> games)
        {
            Console.WriteLine("Enter a game publisher to find out its data");
            string temp = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(temp))
            {
                Console.WriteLine("Please enter a VALID publisher");
                temp = Console.ReadLine();
            }



            List<VideoGame> publisherSpesific = PublisherSort(temp, games);

            foreach (var game in publisherSpesific)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine(ResponseGenerator(games, publisherSpesific, "Developed by",temp,""));
        }



        private static void GenereData(List<VideoGame> games)
        {
            Console.WriteLine("Enter a game genere to find out its data");
            string temp = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(temp))
            {
                Console.WriteLine("Please enter a VALID genere");
                temp = Console.ReadLine();
            }

            

            List<VideoGame> genereSpesific = GenereSort(temp, games);

            foreach (var game in genereSpesific)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine(ResponseGenerator(games, genereSpesific, "", temp, "games"));

        }



























        //I know I overcomplicated this and it's in the banishment zone for a reason 
        //returns a string list of every distinct publisher 
        static List<string> PossibleAnswers(List<VideoGame> games)
        {
            var results = games.DistinctBy(games => games.Publisher);
            List<string> msg = new List<string>();

            foreach (var publisher in results)
            {
                msg.Add($"{publisher.Publisher}"); 
            }
            return msg;

        }


    }
}