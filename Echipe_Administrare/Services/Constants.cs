using System;

public static class Constants
{
    public const int MinNameLength = 2;
    public const int MaxNameLength = 50;
    public const decimal MinSalary = 1500;
    public const decimal MaxSalary = 50000;
    public static readonly DateTime MinBirthDate = new DateTime(1980, 1, 1);
    public static readonly DateTime MaxBirthDate = DateTime.Today.AddYears(-16);
}