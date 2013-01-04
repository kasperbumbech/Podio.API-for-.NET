using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API
{
    public sealed class ParticipationStatus
    {
        private readonly string name;
        private readonly int value;

        public static readonly ParticipationStatus Invited = new ParticipationStatus(0, "invited");
        public static readonly ParticipationStatus Accepted = new ParticipationStatus(1, "accepted");
        public static readonly ParticipationStatus Declined = new ParticipationStatus(2, "declined");
        public static readonly ParticipationStatus Tentative = new ParticipationStatus(3, "tentative");

        private ParticipationStatus(int value, string name)
        {
            this.name = name;
            this.value = value;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
