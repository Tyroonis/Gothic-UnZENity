using System.Linq;
using GUZ.Core.Extensions;
using GUZ.Core.Globals;
using GUZ.Core.Manager;
using UnityEngine;

namespace GUZ.Core.Npc.Actions.AnimationActions
{
    public class StartProcessInfos : AbstractAnimationAction
    {
        private int _dialogId => Action.Int0;

        public StartProcessInfos(AnimationAction action, GameObject npcGo) : base(action, npcGo)
        {
        }

        public override void Start()
        {
            IsFinishedFlag = true;

            // AI_STopProcessInfos was called before this Action
            if (!GameData.Dialogs.IsInDialog)
            {
                return;
            }

            var isInSubDialog = GameData.Dialogs.CurrentDialog.Options.Any();

            if (isInSubDialog)
            {
                var foundItem = GameData.Dialogs.CurrentDialog.Options
                        .FirstOrDefault(option => option.Function == _dialogId);

                // If a dialog calls Info_ClearChoices(), then the current sub dialog is already gone.
                if (foundItem != null)
                {
                    GameData.Dialogs.CurrentDialog.Options.Remove(foundItem);
                }
            }
            else
            {
                // The dialog wasn't important and has no sub-options. i.e. the dialog is fully told.
                if (GameData.Dialogs.CurrentDialog.Instance.Permanent == 0 &&
                    GameData.Dialogs.CurrentDialog.Options.IsEmpty())
                {
                    Props.Dialogs.Remove(GameData.Dialogs.CurrentDialog.Instance);
                }
            }

            DialogManager.StartDialog(NpcGo, Props, false);
        }
    }
}
