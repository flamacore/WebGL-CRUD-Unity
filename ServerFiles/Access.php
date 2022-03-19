<?php
include "DbConn.php";
include "EasyPDO.php";
if(isset($_POST["FunctionToCall"]))
{
    if($_POST["FunctionToCall"] == "GetColumnNames")
    {
        $result = GetColumnNames($_POST["TableName"]);
        $result = json_encode($result, JSON_FORCE_OBJECT);
        echo $result;
    }
    if($_POST["FunctionToCall"] == "GetTableNames")
    {
        $result = GetTableNames();
        $result = json_encode($result, JSON_FORCE_OBJECT);
        echo $result;
    }
    if($_POST["FunctionToCall"] == "GetDataFromTable")
    {
        $result = TryGetEntireDataFromTable($_POST["TableName"]);
        $result = json_encode($result, JSON_FORCE_OBJECT);
        echo $result;
    }
    if($_POST["FunctionToCall"] == "DeleteDataFromTable")
    {
        $result = TryDeleteFromTable($_POST["TableName"], "id ='". $_POST["id"] ."'");
        echo $result;
    }
    if($_POST["FunctionToCall"] == "UpdateTableData")
    {
        $result = TryUpdateTable($_POST["TableName"], $_POST["Columns"], $_POST["Values"], "id ='". $_POST["id"] ."'");
        echo $result;
    }
    if($_POST["FunctionToCall"] == "InsertDataToTable")
    {
        $result = TryInsertToTable($_POST["TableName"], $_POST["Columns"], $_POST["Values"]);
        echo $result;
    }
}
?>
