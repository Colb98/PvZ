public enum AttackType
{
    Melee,
    Range,
    None
}

public enum TargetType
{
	// Single Creature
	Single,
	// Multiple Creatures
	Multiple,
	// Circular Area
	Area,
	// MultipleTiles
	MultipleTiles,
	// Map
	Global
}

public enum TargetIdentity
{
	Determined,
	OnHit,
	None
}