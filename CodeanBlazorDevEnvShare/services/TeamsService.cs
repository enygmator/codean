using CodeanBlazorDevEnvShare.Data;
using CodeanBlazorDevEnvShare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeanBlazorDevEnvShare.services
{
    public interface ITeamsService
    {
        List<(string, bool)> GetTeamsList(string name);
        void AddUserToTeam(string TeamName, string UserName, bool IsAdmin);
        void CreateTeam(string TeamName, string UserName);
        void DeleteTeam(string TeamName);
        void AddRepoToTeam(string TeamName, string RepoName);
        List<string> GetReposList(string TeamName);
    }

    public class TeamsService : ITeamsService
    {
        private readonly ApplicationDbContext _context;

        public TeamsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<(string, bool)> GetTeamsList(string UserName)
        {
            IEnumerable<string> Query1 =
            from team in _context.Teams
            from member in team.Members
            where member.Name == UserName
            select team.Name;

            IEnumerable<bool> Query2 =
            from team in _context.Teams
            from member in team.Members
            where member.Name == UserName
            select member.IsAdmin;

            IEnumerable<(string, bool)> Query3 =
            from team in Query1
            from IsAdmin in Query2
            select (team, IsAdmin);

            return Query3.ToList();
        }

        public List<string> GetReposList(string TeamName)
        {
            IEnumerable<Team> Query1 =
            from team in _context.Teams
            where team.Name == TeamName
            select team;

            Team t = Query1.FirstOrDefault();
            if (t != null)
            {
                IEnumerable<string> Query2 =
                from repo in t.Repos
                select repo.Name;

                return Query2.ToList();
            }
            else
            {
                return null;
            }
        }

        public void AddUserToTeam(string TeamName, string UserName, bool _IsAdmin)
        {
            IEnumerable<Team> Query1 =
            from team in _context.Teams
            where team.Name == TeamName
            select team;

            Team t = Query1.FirstOrDefault();
            if(t!=null)
            {
                IEnumerable<Member> Query2 =
                from member in t.Members
                where member.Name == UserName
                select member;

                Member m = Query2.FirstOrDefault();
                if (m == null)
                {
                    t.Members.Add(new Member { IsAdmin = _IsAdmin, Name = UserName });

                    _context.SaveChanges();
                }
            }
        }

        public void CreateTeam(string TeamName, string UserName)
        {
            List<Member> members = new List<Member>();
            members.Add(new Member { Name = UserName, IsAdmin = true });
            List<Repo> repos = new List<Repo>();
            Team team = new Team { Name = TeamName, Members = members, Repos = repos };
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(string TeamName)
        {
            IEnumerable<Team> Query1 =
            from team in _context.Teams
            where team.Name == TeamName
            select team;

            Team t = Query1.FirstOrDefault();
            if (t != null)
            {
                _context.Teams.Remove(t);
                _context.SaveChanges();
            }
        }

        public void AddRepoToTeam(string TeamName, string RepoName)
        {
            IEnumerable<Team> Query1 =
            from team in _context.Teams
            where team.Name == TeamName
            select team;

            Team t = Query1.FirstOrDefault();
            if (t != null)
            {
                IEnumerable<Repo> Query2 =
                from repo in t.Repos
                where repo.Name == RepoName
                select repo;

                Repo r = Query2.FirstOrDefault();
                if (r == null)
                {
                    t.Repos.Add(new Repo { Name = RepoName });

                    _context.SaveChanges();
                }
            }
        }
    }
}
