using Tavern.Character;

namespace Tavern.Infrastructure
{
    public class HitPointSerializer
    {
        private readonly ICharacter _character;

        public HitPointSerializer(ICharacter character)
        {
            _character = character;
        }

        public string Serialize()
        {
            return _character.GetHpComponent().CurrentHp.ToString();
        }

        public void Deserialize(string value)
        {
            if (!int.TryParse(value, out int hp)) return;
            
            _character.GetHpComponent().Set(hp);
        } 
    }
}