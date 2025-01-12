/*
* PROJECT:          HontelOS
* CONTENT:          Users class
* PROGRAMMERS:      Jort van Dalen
*/

using HontelOS.System.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HontelOS.System.User
{
    public class User
    {
        public static string Username;
        
        public static bool Login(string username, string password)
        {
            var usersFile = File.ReadAllLines("0:\\HontelOS\\users.ini");

            if (usersFile.Contains($"{username};{SHA256.hash(password)}"))
            {
                Username = username;
                return true;
            }
            return false;
        }

        public static void Logout()
        {
            Username = "";
        }

        public static bool Create(string username, string password)
        {
            if (!File.Exists("0:\\HontelOS\\users.ini"))
                File.WriteAllText("0:\\HontelOS\\users.ini", null);

            List<string> usersFile = File.ReadAllLines("0:\\HontelOS\\users.ini").ToList();

            foreach (string line in usersFile)
                if (line.Split(';')[0] == username)
                    return false;

            Directory.CreateDirectory($"0:\\Users\\{username}\\Applications");
            Directory.CreateDirectory($"0:\\Users\\{username}\\Documents");
            Directory.CreateDirectory($"0:\\Users\\{username}\\Downloads");
            Directory.CreateDirectory($"0:\\Users\\{username}\\Desktop");

            usersFile.Add($"{username};{SHA256.hash(password)}");

            File.WriteAllLines("0:\\HontelOS\\users.ini", usersFile.ToArray());

            return true;
        }

        public static void Delete(string username)
        {
            var usersFile = File.ReadAllLines("0:\\HontelOS\\users.ini");
            List<string> newFile = new List<string>();

            foreach (string line in usersFile)
                if (line.Split(';')[0] != username)
                    newFile.Add(line);

            File.WriteAllLines("0:\\HontelOS\\users.ini", newFile.ToArray());
        }

        public static string[] GetUsers()
        {
            var usersFile = File.ReadAllLines("0:\\HontelOS\\users.ini");
            List<string> users = new List<string>();

            foreach(string line in usersFile)
                users.Add(line.Split(';')[0]);

            return users.ToArray();
        }
    }
}
