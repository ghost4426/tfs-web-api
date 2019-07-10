const GET = "GET";
const POST = "POST";
const PUT = "PUT";
const JSON_DATATYPE = "JSON";


const BASE_ADMIN_API_URI = "https://localhost:4200/";
const BASE_COMMON_API_URI = "https://localhost:4201/";

const BASE_ADMIN_URI = "api/admin/";
const BASE_PROVIDER_URI = "api/provider/";
const BASE_FAMR_URI = "api/farm/";
const BASE_COMMON_URI = "api/common/";

const GET_FOOD_DETAIL_TYPE_URI = BASE_COMMON_API_URI + BASE_COMMON_URI + "productdetailtype";
const GET_FOOD_CATEGORY_URI = BASE_COMMON_API_URI + BASE_FAMR_URI + "getAllCategory";
const GET_FARM_FOOD_URI = BASE_COMMON_API_URI + BASE_FAMR_URI + "getByFarmer";
const ADD_FOOD_FEEDING_DATA_URI = BASE_COMMON_API_URI + BASE_COMMON_URI + "feedings/";