# Test Difficulties

## can't mock / substitute mono behavior

component require a class which is not possible to mock / substitute. in order to be able to use substitute / mock you require basic class which later could be attached to another component as a field

## abstract mono behavior Test unavailability

since we can't even substitute mono behavior resulting abstract mono behavior not testable able leaving logic in Abstract mono behavior not testable

## assigning value in start / awake might not be possible with encapsulated field

mono behavior with non-public field can't be assigned. and if it's assigned in start / awake it requires Start to be called

## fully encapsulated behavior ->  zero test

it creates simple well arranged class, but result in 0 testability as is contained and the only thing that calls it are unity
