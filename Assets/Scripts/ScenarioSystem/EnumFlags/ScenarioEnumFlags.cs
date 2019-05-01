using System;

public enum ScenarioEnumFlags
{
    None        = (1 << 0),
    GroteZaal   = (1 << 1),
    Toren       = (1 << 2),
    Poort       = (1 << 3)
}