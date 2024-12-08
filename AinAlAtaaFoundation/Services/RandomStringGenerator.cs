using AinAlAtaaFoundation.Shared.Abstraction;
using System;
using System.Linq;

namespace AinAlAtaaFoundation.Services
{
    internal class RandomStringGenerator : IRandomStringGenerator
    {
        public string Gnerate(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        private static Random _random = new Random();
    }
}
