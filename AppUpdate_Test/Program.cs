// See https://aka.ms/new-console-template for more information

using AppUpdate;
using AppUpdate.Services;

var github = new GitHubApi("textGamex", "HOI_Error_Tools", new AppVersion("v1.0-bate"));
Console.WriteLine(await github.HasLatestAsync());
Thread.Sleep(1000);
