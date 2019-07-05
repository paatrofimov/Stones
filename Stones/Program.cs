using System;
using System.Collections.Generic;
using System.Linq;

namespace Stones
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 50;
            var moves = new[] {3, 4, 7, 8, 10};

            Console.WriteLine("Если я хожу и в кучке указанное количество камней, то для меня");

            var pileToWinStatus = new Dictionary<int, bool>();
            var hopeLessPiles = new HashSet<int>();
            for (var pile = 0; pile <= n; pile++)
            {
                if (IsHopeLessPile(pile, moves, pileToWinStatus))
                {
                    hopeLessPiles.Add(pile);
                }

                pileToWinStatus[pile] = GetWinStatus(pile, moves, hopeLessPiles);

                Console.WriteLine(pileToWinStatus[pile] ? $"- кучка {pile} выигрышна" : $"- кучка {pile} проигрышна");
            }

            Console.WriteLine("Если в кучке {0} камней, тот кто начинает играть, тот {1}",
                              n,
                              pileToWinStatus[n] ? "выигрывает" : "проигрывает");
            Console.ReadLine();
        }

        private static bool IsHopeLessPile(int pile,
                                           int[] moves,
                                           Dictionary<int, bool> pileToWinStatus)
        {
            return PileIsTooSmall(pile, moves) || AllMovesLeadToOpponentVictory(pile, moves, pileToWinStatus);
        }

        private static bool AllMovesLeadToOpponentVictory(int pile, int[] moves, Dictionary<int, bool> pileToWinStatus)
        {
            return moves.All(move => pileToWinStatus[pile - move]);
        }

        private static bool PileIsTooSmall(int pile, int[] moves)
        {
            return pile < moves.Min();
        }

        private static bool GetWinStatus(int pile, int[] moves, HashSet<int> hopeLessPiles)
        {
            return CanTakeWholePile(pile, moves) || CanLeadOpponentToHopelessPile(pile, moves, hopeLessPiles);
        }

        private static bool CanTakeWholePile(int pile, int[] moves)
        {
            return moves.Any(move => move == pile);
        }

        private static bool CanLeadOpponentToHopelessPile(int pile, int[] moves, HashSet<int> hopeLessPiles)
        {
            return moves.Any(move => hopeLessPiles.Contains(pile - move));
        }
    }
}