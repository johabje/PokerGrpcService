﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGrpc.Models
{
    public class HandRanking
    {
        /*
        public int gamePin;
        public Deck deck;
        public List<Player> players = new  List<Player>();
        public Player toAct;
        public List<Card> tableCards;
        public double pot;
        public double bet;
        public int blind;
        */

        /*
        public int hasStraigthFlush(List<Card> hand, List<Card> table)
        {
            hand.AddRange(table);
            List<int> ranks = new List<int>();
            IList<char> suits = new List<char>();
            foreach (Card card in hand)
            {
                ranks.Add(card.rank);
                suits.Add(card.suit);
            }
            ranks.Sort(); //does it sort rigth??

            if (suits.Where(x => x.Equals('H')).Count() >= 5 || suits.Where(x => x.Equals('S')).Count() >= 5
                || suits.Where(x => x.Equals('K')).Count() >= 5 || suits.Where(x => x.Equals('R')).Count() >= 5)
            {
                // if is stragith with the five or more flush cards
                //      return "Stragith flush with
               // else
                //{
                    //find the color of the flush and the higest card, return flush, HC
                //}
            }
            else
            {
                return hasQuads(ranks, suits);
            }
            return 0;
        }

        public int hasQuads(List<int> ranks, IList<char> suits)
        {
            Dictionary<int, int> numberOfEachRank =
                new Dictionary<int, int>();
            List<int> tripps = new List<int>();
            List<int> pairs = new List<int>();
            foreach (int rank in ranks)
            {
                try
                {
                    numberOfEachRank.Add(rank, rank);
                }
                catch (ArgumentException)
                {
                    numberOfEachRank[rank] = rank + 1;
                }
            }
            foreach (var item in numberOfEachRank)
            {
                if (item.Value == 4)
                    return 74 + item.Key;
                else if (item.Value == 3)
                {
                    tripps.Add(item.Key);
                }
                else if (item.Value == 2)
                {
                    pairs.Add(item.Value);
                }
            }
            tripps.Sort(); pairs.Sort();
            if (tripps.Count() > 0)
            {
                if (pairs.Count() > 0)
                {
                    return 1; //Full House pairs[-1] and tripps[-1]
                }
                else
                {
                    return 50 + tripps[-1];
                }
            }

            return 0;
        }
        */
    
        
    }
}
