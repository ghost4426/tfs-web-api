pragma solidity >=0.4.0;

contract FoodDataStorage {

    address private coinBaseAddress = 0xa474456270AF10d32ca235F351904C8c0ca7d4CA;

    struct FarmFoodData{
        string data;
        string newestData;
        string lastUpdateBy;
        bool isValid;
        bool isHasData;
    }

    mapping(uint => FarmFoodData) foodDatas;

    function addNewData(uint id, string memory data, string memory sender) public {
        require(msg.sender == coinBaseAddress);
        require(!foodDatas[id].isHasData);
        foodDatas[id].data = data;
        foodDatas[id].newestData = data;
        foodDatas[id].lastUpdateBy = sender;
        foodDatas[id].isValid = true;
        foodDatas[id].isHasData = true;
    }
    
    function saveData(uint id, string memory data, string memory sender, string memory newData) public {
        require(msg.sender == coinBaseAddress);
        require(foodDatas[id].isValid);
        require(foodDatas[id].isHasData);
        foodDatas[id].data = data;
        foodDatas[id].newestData = newData;
        foodDatas[id].lastUpdateBy = sender;
    }

    function setIsValid(uint id) public{
        require( msg.sender == coinBaseAddress);
        foodDatas[id].isValid = false;
    }

    function getDataById(uint id) public view returns (string memory) {
             return foodDatas[id].data;
    }
}