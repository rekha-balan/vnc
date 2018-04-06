    {
        $c = 0;
        "In Begin, c is $c, x is $x" 
    }
    process
    {
        $c++
        #"In PProcess, c is $c, x is $x, `$_ is $_"
    }
    end
    {
        "In End, c is $c, x is $x"
    }