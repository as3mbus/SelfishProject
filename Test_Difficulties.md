# Test Difficulties

## can't mock / substitute mono behavior

component require a class which is not possible to mock / substitute. in order to be able to use substitute / mock you require basic class which later could be attached to another component as a field

## abstract mono behavior Test unavailability

since we can't even substitute mono behavior resulting abstract mono behavior not testable able leaving logic in Abstract mono behavior not testable

## assigning value in start / awake might not be possible with encapsulated field

mono behavior with non-public field can't be assigned. and if it's assigned in start / awake it requires Start to be called

## fully encapsulated behavior ->  zero test

it creates simple well arranged class, but result in 0 testability as is contained and the only thing that calls it are unity

## Unit Test Mono Behavior that contain polymorphism of mono behavior require separation of logic to another class

as there are no known way to substitute mono behavior resulting in inability of using polymorphism attribute that inherited from mono behavior in another mono behavior. Such condition limit unity unit test capability when polymorphism attribute's are required. Way to resolve this issue is by creating control class that contain logic of the mono behavior. these control class would later be used as test target, keep in mind that in some case having behavior specific logic might be required to provide extendability of such behavior (e.g requiring additional mono behavior / component)
