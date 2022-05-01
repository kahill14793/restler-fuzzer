// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

/// Main program to analyze a Swagger specification and generate a RESTler fuzzing grammar

open Microsoft.FSharpLu
open Restler.Config
open System.IO
open FSharp.Data

let usage() =
    printfn "Usage: dotnet RestlerCompilerExe.dll <path to config file>"
    let sampleConfig = Json.Compact.serialize SampleConfig
    printfn "Sample config:\n %s" sampleConfig

[<EntryPoint>]
let main argv =

    // let config =
    //     match argv with
    //     | [|configFilePath|] ->
    //         if File.Exists configFilePath then
    //             let config = Json.Compact.deserializeFile<Config> configFilePath
    //             let config =
    //                 match config.GrammarOutputDirectoryPath with
    //                 | None ->
    //                     raise (exn("'GrammarOutputDirectoryPath' must be specified in the config file."))
    //                 | Some _ -> config
    //             convertRelativeToAbsPaths configFilePath config
    //         else
    //             printfn "Path not found: %s" configFilePath
    //             exit 1
    //     | _ ->
    //         usage()
    //         exit 1


    // thond: thu doc file csv
    let msft1 = CsvFile.Load("C:\\Users\\Admin\\Desktop\\output_log_analyze.csv").Cache()
    for row in msft1.Rows |> Seq.truncate 10 do
        printfn "HLOC: (%s, %s, %s)" (row.[0]) (row.[1]) (row.[2])


    // thond: set path de debug
    let path = "D:\\study\\thesis\\my_project\\temp\\testing\\demo\\Compile\\config.json";

    let config =
        if File.Exists path then
            let config = Json.Compact.deserializeFile<Config> path
            let config =
                match config.GrammarOutputDirectoryPath with
                | None ->
                    raise (exn("'GrammarOutputDirectoryPath' must be specified in the config file."))
                | Some _ -> config
            convertRelativeToAbsPaths path config
        else
            printfn "Path not found: %s" path
            exit 1

    Restler.Workflow.generateRestlerGrammar None config
    0