# Code Computerlove pre-screening exercise 1

This is a simple project that asks the user for their name and then tells them their fortune.

Please complete the following steps:

1. Write some unit tests to cover the existing functionality.

2. Show the user the fortune on the day they were born.

E.G.
```
What's your name? Bob
When were you born (dd/mm/yyyy)? 01/01/1980
Hi Bob!
Your fortune for today is: Newlyweds should be avoided!
On the day you were born your fortune was: Beware of figs!
```

3. If today is the user's birthday wish them a happy birthday.

E.G.
```
What's your name? Bob
When were you born (dd/mm/yyyy)? 01/01/1980
Happy birthday, Bob!
Your fortune for today is: Newlyweds should be avoided!
On the day you were born your fortune was: Beware of figs!
```

## Notes and hints

You can use any unit testing or mocking libraries of your choice.

You will probably need to wrap `Console` in order to get it under test. See `DateTimeOffsetWrapper` for an example.
