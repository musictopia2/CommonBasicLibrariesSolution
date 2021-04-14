﻿using CommonBasicLibraries.CollectionClasses;
using System;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator
{
    public interface IRandomGenerator
    {
        //this is everything that is needed.  this means it can be used in the entire system and later can figure out the details for it.
        //was going to do formula.  however, that does not fit in the interface.
        string GenerateRandomPassword();
        string GenerateRandomPassword(RandomPasswordParameterClass thisPassword);
        int GetSeed(); //often times we do need to get the seed of this.
        int GetRandomNumber(int maxNumber, int startingPoint = 1, BasicList<int>? previousList = null);
        BasicList<int> GenerateRandomList(int maxNumber, int howMany = -1, int startingNumber = 1, BasicList<int>? previousList = null, BasicList<int>? setToContinue = null, bool putBefore = false);
        BasicList<int> GenerateRandomNumberList(int maximumNumber, int howMany, int startingPoint = 0, int increments = 1);
        string NextString(int howMany, string stringsToPick);
        int NextAge(EnumAgeRanges types = EnumAgeRanges.Adult);
        BasicList<string> GetSeveralUniquePeople(int howMany);
        string NextFirstName(bool isFemale = false);
        string NextAnyName();
        string NextLastName();
        string NextGender(); //prefer to use gender now.
        //since i now have full name, decided to not worry about this part.  if a person still wants it, they can always cast back and use.
        //string NextName(bool isMale = false, bool middle = false);
        string NextSSN(bool ssnFour = false, bool dashes = true);
        string NextDomainName(string? tld = null);
        string NextTopLevelDomain();
        string NextEmail(string? domain = null, int length = 7);
        string NextHashtag();
        string NextIP();
        string NextTwitterName();
        string NextUrl(string protocol = "http", string? domain = null, string? domainPrefix = null, string? path = null, BasicList<string>? extensions = null);
        string NextColor();
        string NextZipCode(bool plusfour = true);
        string NextStreet(int syllables = 2, bool shortSuffix = true);
        //risk not doing next state since i am rethinking this part now.  if needed, rethink then.
        //string NextState(bool fullName = false);
        double NextLongitude(double min = -180.0, double max = 180.0, uint decimals = 5);
        double NextLatitude(double min = -90, double max = 90, uint decimals = 5);
        string NextGeohash(int length = 7);
        string NextCity();
        string NextAddress(int syllables = 2, bool shortSuffix = true);
        int NextYear(int min = 2000, int max = -1);
        string NextMonth(int min = 1, int max = 12);
        int NextSecond();
        int NextMinute(int min = 0, int max = 59);
        int NextHour(bool twentyfourHours = true, int? min = null, int? max = null);
        int NextMillisecond();
        DateTime NextDate(DateTime? min = null, DateTime? max = null, bool simple = false);
        long NextCreditCardNumber(string? cardType = null);
        string NextGUID(int version = 5);
        bool NextBool(int likelihood = 50);
        FullNameClass NextFullName();
        void SetRandomSeed(int value);
        ITestPerson GetTestSinglePerson<T>(EnumAgeRanges defaultAge = EnumAgeRanges.Adult) where T : ITestPerson, new();
        BasicList<ITestPerson> GetTestPeopleList<T>(int HowMany, EnumAgeRanges DefaultAge = EnumAgeRanges.Adult) where T : ITestPerson, new();
    }
}