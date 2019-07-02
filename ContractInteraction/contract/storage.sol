pragma solidity >=0.4.0 <0.7.0;

contract FoodDataStorage {

    struct FoodData{
        string data;
    }

     mapping(uint => FoodData) foodDatas;

    function saveData(string memory data, uint id) public {
        foodDatas[id].data = data;
    }

    function getDataById(uint id) public view returns (string memory) {
        return foodDatas[id].data;
    }
}