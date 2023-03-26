using System;
using System.Text;
using System.Text.RegularExpressions;
using ICanHazDadJoke_Search.Model;

namespace ICanHazDadJoke_Search.Common
{
    internal class Utilities
    {
        internal static StringBuilder SetSearchContent(ICanHazDadJoke jokes, string searchTerm)
        {
            StringBuilder allContents = new StringBuilder();
            StringBuilder shortJokesContent = new StringBuilder();
            StringBuilder mediumJokesContent = new StringBuilder();
            StringBuilder LongJokesContent = new StringBuilder();

            int shortJokesCount = 0;
            int mediumJokesCount = 0;
            int longJokesCount = 0;

            shortJokesContent.AppendLine("Short jokes (<10 words):");
            shortJokesContent.AppendLine("------------------------------");
            mediumJokesContent.AppendLine("Medium jokes (<20 words):");
            mediumJokesContent.AppendLine("------------------------------");
            LongJokesContent.AppendLine("Long jokes (>= 20 words):");
            LongJokesContent.AppendLine("------------------------------");

            foreach (ICanHazDadJokeItem joke in jokes.Results)
            {
                int numberOfWords = WordCount(joke.Joke);

                if (numberOfWords < 10)
                {
                    shortJokesContent.AppendLine(FormatJoke(joke.Joke, searchTerm));
                    shortJokesCount++;
                }
                else if (numberOfWords < 20)
                {
                    mediumJokesContent.AppendLine(FormatJoke(joke.Joke, searchTerm));
                    mediumJokesCount++;
                }
                else if (numberOfWords >= 20)
                {
                    LongJokesContent.AppendLine(FormatJoke(joke.Joke, searchTerm));
                    longJokesCount++;
                }
            }

            if (shortJokesCount != 0)
            {
                allContents.Append(shortJokesContent);
            }
            else
            {
                shortJokesContent.AppendLine("***Your search term resulted in no short jokes.***");
                allContents.Append(shortJokesContent);
            }

            allContents.AppendLine();

            if (mediumJokesCount != 0)
            {
                allContents.Append(mediumJokesContent);
            }
            else
            {
                mediumJokesContent.AppendLine("***Your search term resulted in no medium jokes.***");
                allContents.Append(mediumJokesContent);
            }

            allContents.AppendLine();

            if (longJokesCount != 0)
            {
                allContents.Append(LongJokesContent);
            }
            else
            {
                LongJokesContent.AppendLine("***Your search term resulted in no long jokes.***");
                allContents.Append(LongJokesContent);
            }

            return allContents;
        }

        #region Private methods

        private static string FormatJoke(string joke, string searchTerm)
        {
            string formattedJoke = string.Empty;

            if (!string.IsNullOrWhiteSpace(joke))
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    if (Regex.IsMatch(joke, searchTerm, RegexOptions.IgnoreCase))
                    {
                        var matchedWord = Regex.Match(joke, searchTerm, RegexOptions.IgnoreCase);

                        formattedJoke = Regex.Replace(joke, matchedWord.Value, $"<{ searchTerm.ToUpper()}>");
                    }
                    else
                    {
                        formattedJoke = joke;
                    }
                }
                else
                {
                    formattedJoke = joke;
                }
            }

            return formattedJoke;
        }
        private static int WordCount(string sentence)
        {
            int wordCount = 0;
            for (int i = 0; i < sentence.Length - 1; i++)
            {
                if (Char.IsWhiteSpace(sentence[i]) && !Char.IsWhiteSpace(sentence[i + 1]) && i > 0)
                {
                    wordCount++;
                }
            }

            return ++wordCount;
        }
        #endregion
    }
}
