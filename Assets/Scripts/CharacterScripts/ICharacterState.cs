using UnityEngine;

namespace CharacterScripts
{
    public interface ICharacterState
    {
        public void OnClick(MainControl state,Vector3 point);
        /// <summary>
        /// Calls every update from MainControl
        /// </summary>
        public void Update();
    }
}