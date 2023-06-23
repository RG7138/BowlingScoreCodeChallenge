namespace BowlingScoreCodeChallenge
{
    internal class Program
    {
        //Cosmetic function for visual representation purpose
        static String FrameScoreCountFunc(int Score, String FrameScoreCount)
        {
           
            int NumberOfDigits = 1;

            if ((Score / 10) > 0)
            {
                NumberOfDigits = 2;
            }
            else if ((Score / 100)>0) 
            {
                NumberOfDigits = 3;
            }


            switch (NumberOfDigits){
                case 1:
                    FrameScoreCount += FrameScoreCount += Score + "     ";
                    break;
                case 2:
                    FrameScoreCount += Score + "    ";
                    break;
                case 3:
                    FrameScoreCount += Score + "   ";
                    break;
            }

            return FrameScoreCount;
        }

        static void Main(string[] args)
        {
            int frameScore = 0, prevFrame = 0, prevFrameTwo = 0, shot1Input, shot2Input = 0, Score = 0, Frame10ExtraTries; // Extra tries will be based on the fact we have spare or a strike in the first 2 tries on frame10.

            //To keep track of whether we had a strike, spare or a couple of strikes in a row
            bool strike = false, strikeTwo = false, spare = false;
            String VisualRepFrameBoard = "", FrameScoreCount = "", Frame10Shot2 = "", Frame10Shot3 = "", frameNum = "", seperator = "";

            for (int frame=1; frame <= 10; frame++)
            {
                Console.WriteLine("Please Enter your Scores for Frame {0} (Accepted values are between 0 and 10 both inclusive):", frame);
                do//loop for Shot number 1 in the same frame
                {
                    Console.Write("Shot 1 Score:");
                    shot1Input = int.Parse(Console.ReadLine());
                } while (shot1Input > 10 || shot1Input < 0); //checks for valid input. Otherwise we continue to loop through unless until a valid input is entered.


                if (spare == true)// if previous frame was a spare add in the extra points now
                {
                    prevFrame = 10 + shot1Input;
                    spare = false;
                    Score = prevFrame + Score;
                    FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);

                }
                
                //Below If-else block is to check if the we had 2 strikes in row and we hit another strike
                if (strikeTwo == true && shot1Input == 10)
                {
                    prevFrameTwo = 30; //Because our previous 2 tries resulted in a strike so adding 20 plus the 10 on the current strike.
                    Score = prevFrameTwo + Score;
                    FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                }
                else if (strikeTwo == true && shot1Input != 10)
                {
                    strikeTwo = false;
                    prevFrameTwo = 10 + 10 + shot1Input;
                    Score = prevFrameTwo + Score;
                    FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                }


                if (strike == true && shot1Input == 10)
                {
                    strikeTwo = true;
                    prevFrameTwo = 20;
                }
               
                    if (shot1Input < 10) //check to make sure there wasn't a strike on first bowl
                    {
                        do //loop for Shot number 2 in the same frame
                        {
                            Console.Write("Shot 2 Score:");
                            shot2Input = int.Parse(Console.ReadLine());
                        } while (shot2Input > (10 - shot1Input) || shot2Input < 0); //checks for valid input. Otherwise we continue to loop through unless until a valid input is entered.
                    if (shot1Input + shot2Input == 10)
                        {
                            spare = true;
                            VisualRepFrameBoard += shot1Input + "-/ | ";
                        }

                        if (strikeTwo == true && frame == 10)
                        {
                            prevFrameTwo = 10 + 10 + shot2Input;
                            Score = prevFrameTwo + Score;
                            FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                            strikeTwo = false;
                        }

                        if (strike == true && shot1Input != 10)
                        {
                            strike = false;
                            prevFrame = 10 + shot1Input + shot2Input;
                            Score = Score + prevFrame;
                            FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                        }
                        if (spare != true && strike != true && strikeTwo != true)
                        {
                            frameScore = shot1Input + shot2Input;
                            Score = Score + frameScore;
                            FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                            if (frame != 10)
                                VisualRepFrameBoard += " " + shot1Input + "-" + shot2Input + " |";
                            else
                                VisualRepFrameBoard += " " + shot1Input + "-" + shot2Input;
                        }
                    }
                    else
                    {
                        strike = true;
                        prevFrame = 10;
                        if (frame != 10)
                            VisualRepFrameBoard += " X-  |";
                    }
                

                //This is to check if we had a spare or strike in our last frame , so that we can accomodate the chance of another shot.
                if (frame == 10 && strike == true)
                {
                    do
                    {
                        Console.Write("Shot 2 Score:");
                        shot2Input = int.Parse(Console.ReadLine());
                    } while (shot2Input < 0 || shot2Input > 10);

                    if (strikeTwo == true)
                    {
                        prevFrameTwo = 10 + 10 + shot2Input;
                        Score = prevFrameTwo + Score;
                        FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                        strikeTwo = false;
                    }
                }

                if (frame == 10 && (spare == true || strike == true))
                {
                    do
                    {
                        Console.Write("Shot 3 Score:");
                        Frame10ExtraTries = int.Parse(Console.ReadLine());
                    } while (Frame10ExtraTries < 0 || Frame10ExtraTries > 10);
                    if (strike == true)
                    {
                        prevFrame = 10 + shot2Input + Frame10ExtraTries;
                        Score = Score + prevFrame;
                        FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                        if (shot2Input == 10)
                            Frame10Shot2 = "-X";
                        else
                            Frame10Shot2 += shot2Input;
                        if (Frame10ExtraTries == 10)
                            Frame10Shot3 = "-X";
                        else
                            Frame10Shot3 += Frame10ExtraTries;
                        VisualRepFrameBoard += " X" + Frame10Shot2 + Frame10Shot3;
                    }
                    else
                    {
                        if (Frame10ExtraTries == 10)
                            Frame10Shot3 = "-X";
                        else
                            Frame10Shot3 += Frame10ExtraTries;
                        if (shot2Input + Frame10ExtraTries == 10 && Frame10ExtraTries != 10)
                            Frame10Shot3 = "-/";
                        else
                            Frame10Shot3 += Frame10ExtraTries;
                        Score = Score + 10 + Frame10ExtraTries;
                        FrameScoreCount = FrameScoreCountFunc(Score, FrameScoreCount);
                        VisualRepFrameBoard += shot1Input + "-/" + Frame10Shot3;
                    }
                }
                frameNum += frame + "     ";
                seperator += "------";

                //For printing our frame number the score of the current frame is being updated simultaneously.
                Console.WriteLine("\n");
                Console.WriteLine("Score Board");
                Console.WriteLine(frameNum);
                Console.WriteLine(seperator);
                Console.WriteLine(VisualRepFrameBoard);
                Console.WriteLine(FrameScoreCount);
                Console.WriteLine("\n");
            }
           
        }
    }
}
