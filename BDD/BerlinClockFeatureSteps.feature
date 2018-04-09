
Feature: The Berlin Clock
	As a clock enthusiast
    I want to tell the time using the Berlin Clock
    So that I can increase the number of ways that I can read the time


Scenario: Midnight 00:00
When the time is "00:00:00"
Then the clock should look like
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""


Scenario: Middle of the afternoon
When the time is "13:17:01"
Then the clock should look like
"""
O
RROO
RRRO
YYROOOOOOOO
YYOO
"""

Scenario: Just before midnight
When the time is "23:59:59"
Then the clock should look like
"""
O
RRRR
RRRO
YYRYYRYYRYY
YYYY
"""

Scenario: Midnight 24:00
When the time is "24:00:00"
Then the clock should look like
"""
Y
RRRR
RRRR
OOOOOOOOOOO
OOOO
"""

Scenario: Quarter past 13:15
When the time is "13:15:01"
Then the clock should look like
"""
O
RROO
RRRO
YYROOOOOOOO
OOOO
"""

Scenario: Quarter for 16:45
When the time is "16:45:01"
Then the clock should look like
"""
O
RRRO
ROOO
YYRYYRYYROO
OOOO
"""

Scenario: Half past 06:30
When the time is "06:30:00"
Then the clock should look like
"""
Y
ROOO
ROOO
YYRYYROOOOO
OOOO
"""

Scenario: NotValid 25:00
When the time is "25:00:00"
Then the clock should look like
"""
Input Time format is not correct, shoud be HH:mm:ss
"""

