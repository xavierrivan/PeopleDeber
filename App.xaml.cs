﻿namespace PeopleDeber;

public partial class App : Application
{
    public static PersonRepository PersonRepo { get; private set; }

    public App(PersonRepository repo)
    {
        InitializeComponent();
        PersonRepo = repo;
        MainPage = new AppShell();
    }
}
