using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGrpc.Models
{
    public class HandRanking
    {

        /*
         * 0 - royal flush
         * 1 - straight flush
         * 2 - four of a kind
         * 3 - full house
         * 4 - flush
         * 5 - straight
         * 6 - threes
         * 7 - two pair
         * 8 - one pair
         * 9 - high card
        */
        /*
         * ! BTW !
         *  not tested
         */
        static List<Card> dummyList = new List<Card>();
        static List<List<Card>> dummyListList = new List<List<Card>>();

        public static Tuple<int, List<Card>> GetBestHand(List<Card> handCards, List<Card> tableCards) {
            List<Card> allCards = handCards.Concat(tableCards).ToList();

            HasRoyalFlush(allCards);
            HasXOfAKind(allCards);

            var bigCombos = HasRoyalFlush(allCards);
            int combosScore = bigCombos.Item1;
            List<Card> combosCards = bigCombos.Item2;

            var pairsEtc = HasXOfAKind(allCards, 4);
            var etcScore = pairsEtc.Item1;
            List<Card> etcCards = pairsEtc.Item2;

            int bestHandScore;
            List<Card> usedCards;

            if (combosScore < etcScore) {
                bestHandScore = combosScore;
                usedCards = combosCards;
            } else {
                bestHandScore = etcScore;
                usedCards = etcCards;
            }

            // fill up best hand
            for (int i = usedCards.Count; i < 5; i++) {
                Card nextHighCard = GetHighCard(allCards, usedCards);
                usedCards.Add(nextHighCard);
            }
            return Tuple.Create(bestHandScore, usedCards);
        }

        private static Tuple<int, List<Card>> HasRoyalFlush(List<Card> cards) {
            var straightFlushInfo = HasStraightFlush(cards);
            int straightFlushScore = straightFlushInfo.Item1;
            List<Card> straightFlushCards = straightFlushInfo.Item2;

            if (straightFlushScore == 1 && !straightFlushCards.Where(c => c.rank < 10).Any()) {
                //has royal flush
                return Tuple.Create(0, straightFlushCards);
            } else {
                return Tuple.Create(straightFlushScore, straightFlushCards);
            }
            
        }

        private static Tuple<int, List<Card>> HasStraightFlush(List<Card> cards) {
            var straightInfo = HasStraight(cards);
            int straightScore = straightInfo.Item1;
            List<List<Card>> straightLists = straightInfo.Item2;

            var flushInfo = HasFlush(cards);
            int flushScore = flushInfo.Item1;
            List<Card> flushCards = flushInfo.Item2;

            // cehck if straight has a subset of hasflush = straight flush
            // if straight flush, check if royal
            if (straightScore != 99 && flushScore != 99) {
                foreach (List<Card> straight in straightLists) {
                    if (!straight.Except(flushCards).Any()) {
                        // = the 5 straight cards are a subset of the flush cards = straightflush
                        return Tuple.Create(1, straight);
                    }
                }
            }
            if (flushScore != 99) {
                return Tuple.Create(flushScore, flushCards);
            } else {
                // returns straight score: 99 (bad) if no straight
                return Tuple.Create(straightScore, straightLists.First());
            }
        }
        
        private static Tuple<int, List<List<Card>>> HasStraight(List<Card> cards) {
            List<Card> descendingList = cards.OrderByDescending(card => card.rank).ToList();
            Card[] cardArray = descendingList.ToArray();

            /* 
             * TODO
             * save high card in these functions or check later?
             */

            List<List<Card>> straightList = new List<List<Card>>();
            List<Card> straight;
            bool ggez = false;

            for(int i = 0; i < cardArray.Length; i++) {
                for (int j = 1; j < 5; j++) {
                    int index = i + j;
                    if (index >= cardArray.Length) {
                        index -= cardArray.Length;
                    }

                    int nextCardCheck = cardArray[index].rank - j;

                    if (nextCardCheck < 2) {
                        nextCardCheck += 13;
                    }

                    if (!(cardArray[i].rank == nextCardCheck)) {
                        break;
                    }
                    if (j == 4) {
                        straight = new List<Card>();

                        for (int x = 0; x < 5; x++) {
                            int cardIndex = i + x;
                            if (cardIndex >= cardArray.Length) {
                                cardIndex -= cardArray.Length;
                                straight.Add(cardArray[cardIndex]);
                            }
                        }
                        straightList.Add(straight);
                        ggez = true;
                    }
                }
            }
            if (ggez) {
                return Tuple.Create(5, straightList);
            } else {
                return Tuple.Create(99, dummyListList);
            }
        }

        private static Tuple<int, List<Card>> HasFlush(List<Card> cards) {
            char[] standardSuits = { 'H', 'K', 'S', 'R' };
            List<Card> flushList = new List<Card>();

            foreach (char cs in standardSuits) {
                if (cards.Where(c => c.suit.Equals(cs)).Count() >= 5) {
                    foreach (Card card in cards.Where(ca => ca.suit.Equals(cs))) {
                        flushList.Add(card);
                    }
                    return Tuple.Create(4, flushList);
                }
            }
            return Tuple.Create(99, dummyList);
        }

        private static Card GetHighCard(List<Card> allCards, List<Card> usedCards) {
            List<Card> unusedCards = allCards.Except(usedCards).ToList();
            unusedCards.OrderByDescending(c => c.rank);
            Card highCard = unusedCards.First();
            return highCard;
        }
        
        private static Tuple<int, List<Card>> HasXOfAKind(List<Card> cards, int x = 4) {
            List<Card> xOfKind = new List<Card>(cards.GroupBy(c => c.rank).Count(c => c.Count() == x));
            if (!xOfKind.Any()) {
                // no x-of a rank, check for x-1 if x>2 (no reason to check for x=1, -> high card)
                if (x > 2) {
                    return HasXOfAKind(cards, x - 1);
                } else {
                    return Tuple.Create(99, dummyList);
                }
            } else {
                // save highest combination
                int cardRankMatching = xOfKind.Max(c => c.rank);
                // x cards of a rank has been found
                List<Card> usedCards = new List<Card>();

                foreach (Card card in cards.Where(c => c.rank.Equals(cardRankMatching))) {
                    usedCards.Add(card);
                }
                List<Card> unusedCards = cards.Except(usedCards).ToList();
                if (x == 4) {
                    // four of a kind, best we can get here so dont need to check more
                    return Tuple.Create(2, usedCards);
                } else if (x == 3) {
                    // three of a kind has been found
                    // check if house as well
                    var houseInfo = HasXOfAKind(unusedCards, x - 1);
                    int houseScore = houseInfo.Item1;
                    List<Card> houseCards = houseInfo.Item2;
                    if (houseScore != 99) {
                        // subset of set of available cards excluding those used in three of a kind -
                        // found a pair -> house

                        // create a list for ALL used cards (three of a kind and the pair)
                        List<Card> allUsedCards = new List<Card>(usedCards.Concat(houseCards).ToList());
                        return Tuple.Create(3, allUsedCards);
                    } else {
                        // no house, returning three of a kind
                        return Tuple.Create(6, usedCards);
                    }
                } else {
                    // must be x == 2 (or bad input..)
                    if (cards.Count <= 5) {
                        //.Count <= 5 not checking more cuz max 2 pairs
                        return Tuple.Create(8, usedCards);
                    } else {
                        //.Count <= 5 not checking more cuz max 2 pairs
                        var twoPairInfo = HasXOfAKind(unusedCards, x);
                        int twoPairScore = twoPairInfo.Item1;
                        List<Card> twoPairCards = twoPairInfo.Item2;
                        if (twoPairScore != 99) {
                            List<Card> allUsedCards = twoPairCards.Concat(usedCards).ToList();
                            // returning two pairs
                            return Tuple.Create(7, allUsedCards);
                        } else {
                            return Tuple.Create(8, usedCards);
                        }
                    }
                }
                
            }
        }
        
    }
}
