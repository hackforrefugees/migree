using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class MatchedUser : IComparable<MatchedUser>
    {
        public MatchedUser()
        {
            HasPrimaryCompetence = false;
            HasSecondaryCompetence = false;
            HasThirdCompetence = false;
            IsOnSameLocation = false;
        }
        
        public IUser User { get; set; }
        public bool HasPrimaryCompetence { get; set; }
        public bool HasSecondaryCompetence { get; set; }
        public bool HasThirdCompetence { get; set; }
        public bool IsOnSameLocation { get; set; }

        public int CompareTo(MatchedUser other)
        {
            var userPoints = GetPoint(this);
            var otherUserPoints = GetPoint(other);
            
            if (userPoints != otherUserPoints)
            {
                return otherUserPoints.CompareTo(userPoints);
            }

            if (IsOnSameLocation && !other.IsOnSameLocation)
            {
                return -1;
            }
            else if (!IsOnSameLocation && other.IsOnSameLocation)
            {
                return 1;
            }

            return 0;
        }

        private int GetPoint(MatchedUser user)
        {
            var point =
                (user.HasPrimaryCompetence ? 5 : 0) +
                (user.HasSecondaryCompetence ? 4 : 0) +
                (user.HasThirdCompetence ? 2 : 0);

            return point;
        }
    }
}
