using GoGBot.BLL.Providers.Interfaces;
using System;

namespace GoGBot.BLL.Providers
{
    public class RandomProvider : IRandomProvider
    {
        public int GetRandomInt(int maxValue) => new Random().Next(default, maxValue);

    }
}
