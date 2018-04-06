function add()
{
    param
    (
        [int] $x,
        [int] $y,
        [switch] $red
    )

    "`$red=$red"

    return $x + $y
}


function add1()
{
    param
    (
        $x,
        $y
    )

    return $x + $y
}

function add2($x, $y, [switch]$red)
{
    "`$red=$red"

    return $x + $y
}

function add3([int]$x, [int]$y)
{

    return $x + $y
}


add 1 2
add 1 2 -Red
add1 1 2
add1 1.1 2.2
add1 a b
add1 1 a
add2 1 2
add2 1 2 -Red
add3 1 2

