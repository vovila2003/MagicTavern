using System.Globalization;
using Tavern.Character;

namespace Tavern.Infrastructure
{
    public class SpeedSerializer
    {
        private readonly ICharacter _character;

        public SpeedSerializer(ICharacter character)
        {
            _character = character;
        }

        public string Serialize()
        {
            return _character.GetMoveComponent().Speedable.GetSpeed().ToString(CultureInfo.CurrentCulture);
        }

        public void Deserialize(string value)
        {
            if (!float.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out float speed)) return;
            
            _character.GetMoveComponent().Speedable.SetSpeed(speed);
        } 
    }
}