﻿// =================================
// This file contains the code from the "FP Toolkit" slides
//
// Exercise:
//    look at, execute, and understand all the code in this file
// =================================


// ========================================
// Tool #4 - bind
// ========================================


// ===================================
// Bind for Options
// ===================================

module OptionBind =

    // three Option-returning functions to be chained together
    let doSomething x = Some x  
    let doSomethingElse x = Some x
    let doAThirdThing x = Some x

    let example_v1 input =
        let x = doSomething input
        if x.IsSome then
            let y = doSomethingElse (x.Value)
            if y.IsSome then
                let z = doAThirdThing (y.Value)
                if z.IsSome then
                    let result = z.Value
                    Some result
                else
                    None
            else
                None
        else
            None

    // ----------------------------------
    // define a helper function
    // to make composition easy

    let ifSomeDo f (opt:'a option) =
        if opt.IsSome then
            f opt.Value
        else
            None

    let example_v2 input =
        doSomething input
        |> ifSomeDo doSomethingElse
        |> ifSomeDo doAThirdThing

    // ----------------------------------
    // Or use the built-in Option.bind function

    let example_v3 input =
        doSomething input
        |> Option.bind doSomethingElse
        |> Option.bind doAThirdThing

// ===================================
// Bind for Results
// ===================================

module ResultBind =

    // three Result-returning functions to be chained together
    let doSomething x = if x % 2 = 0 then Ok x else Error "not / by 2"
    let doSomethingElse x = if x % 3 = 0 then Ok x else Error "not / by 3"
    let doAThirdThing x = if x % 5 = 0 then Ok x else Error "not / by 5"

    let example input =
        doSomething input
        |> Result.bind doSomethingElse
        |> Result.bind doAThirdThing

    // test the code
    example 2
    example 6
    example 30

// ===================================
// Bind for Lists
// ===================================

module ListBind =

    // three List-returning functions to be chained together
    let doSomething x = [x+1; x+2]
    let doSomethingElse x = [x+10; x+20]
    let doAThirdThing x = [x+100; x+200]

    // A helper to make things consistent.
    // In F#, bind for lists is List.collect. In C# it is SelectMany
    let listBind = List.collect

    let example input =
        doSomething input
        |> listBind doSomethingElse
        |> listBind doAThirdThing

    // test the code
    example 5

// ===================================
// Bind for Async
// ===================================

module AsyncBind =

    // three Async-returning functions to be chained together
    let doSomething x = async.Return x
    let doSomethingElse x = async.Return x
    let doAThirdThing x = async.Return x

    // helper
    let asyncBind f x = async.Bind(x,f)

    let example input =
        doSomething input
        |> asyncBind doSomethingElse
        |> asyncBind doAThirdThing

