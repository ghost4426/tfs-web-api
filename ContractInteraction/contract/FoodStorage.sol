pragma solidity >=0.4.0;

contract FoodDataStorage {

    address private coinBaseAddress = 0x06012c8cf97BEaD5deAe237070F9587f8E7A266d;

    struct FarmFoodData{
        string data;
        bool isValid;
        bool isHasData;
    }

     mapping(uint => FarmFoodData) foodDatas;

    function saveData(uint id, string memory data) public {
        require(
            msg.sender == coinBaseAddress,
         "Invalid sender address!");

         require(
            foodDatas[id].isValid,
         "Data has isn't valid can't modifile!");

        foodDatas[id].data = data;
    }

    function setValidData(uint id, bool isValid) public{
        require(
            msg.sender == coinBaseAddress,
         "Invalid sender address!");

        foodDatas[id].data = data;
    }

    function getDataById(uint id) public view returns (string memory) {
        return foodDatas[id].data;
    }
}