module FsharpPerson.Domain

open System

type String50 = private String50 of string
module String50 =
    let create propertyName s =
        if String.IsNullOrEmpty(s) || s.Length > 50 then
            Error (propertyName + " must be non-empty and not exceed 50 characters")
        else
            Ok (String50 s)

    let value (String50 s) = s

type Birthdate = private Birthdate of DateTime
module Birthdate =
    let create propertyName d =
        if d = DateTime.MinValue then
            Error (propertyName + "must be specified")
        else
            Ok (Birthdate d)

    let value (Birthdate d) = d

type Person = {
    FirstName: String50
    LastName: String50
    Birthdate: Birthdate option
}
