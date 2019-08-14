# Variant Method found in Development

## Ground Check

### Velocity condition

``` csharp
private bool groundCheck()
{
    if (_rigidBd.velocity.y != 0) return false;
    return true;
}
```

### ObjectCast

``` csharp
private bool groundCheck()
{
    var phys = Physics2D.CircleCastAll(_cld.bounds.center, _cld.bounds.extents.x, Vector2.down, _offset);
    if (phys.Length <= 1) return false;
    return true;
}
```

## Movement

### RigidBody MovePosition

``` csharp
rigidBd.MovePosition(transform.position + Vector3.right * Time.deltaTime);
```

### RigidBody Velocity

```csharp
rigidBd.velocity += Vector2.right * _movementMultiplier * Time.deltaTime;
```
