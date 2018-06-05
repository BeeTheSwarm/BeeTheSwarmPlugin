using System.Collections.Generic;

namespace BTS {
    public class GetInvitationsResponse : PackageResponse {
        public List<InvitationModel> Invitations { get; private set; }
        public int InvitationsCount { get; private set; }

        public override void Parse(Dictionary<string, object> data) {
            Invitations = new List<InvitationModel>();
            var invitationSource = (List<object>)data["invitations"];
            foreach (object obj in invitationSource) {
                InvitationModel invite = new InvitationModel();
                invite.ParseJSON((Dictionary<string, object>)obj);
                Invitations.Add(invite);
            }
            InvitationsCount = int.Parse(data["invitations_count"].ToString());
        }
    }

}