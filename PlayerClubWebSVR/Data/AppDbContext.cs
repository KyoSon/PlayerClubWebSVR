using Microsoft.EntityFrameworkCore;
using PlayerClubWebSVR.Models;

namespace PlayerClubWebSVR.Data
{
    //public class AppDbContext : DbContext
    public class AppDbContext
    {
        public List<Player> Players = new List<Player>();

        public  List<Team> Teams = new List<Team>();

        public  List<RugbyUnion> RugbyUnions= new List<RugbyUnion>();

    }
}
