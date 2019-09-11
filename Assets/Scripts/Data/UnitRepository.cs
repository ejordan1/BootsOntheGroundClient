using System.Collections.Generic;
using Scripts.MapSetup.Services;

namespace Data
{
    public class UnitRepository
    {
        private readonly UnitRegistry _unitRegistry;

		public readonly Dictionary<string, UnitData> _unitsById = new Dictionary<string, UnitData>();

        public UnitRepository(UnitRegistry unitRegistry)
        {
            _unitRegistry = unitRegistry;

            ParseUnits();
			BridgeData.unitRepo = this;
        }

		private void ParseUnits()
        {
            foreach (var unitData in _unitRegistry)
            {
                _unitsById[unitData.Id] = unitData;
            }
        }

        public UnitData GetById(string id)
        {
            return _unitsById[id];
        }
    }
}