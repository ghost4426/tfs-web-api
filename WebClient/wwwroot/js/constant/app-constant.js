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
const CREATE_FOOD_DATA_URI = BASE_COMMON_API_URI + BASE_FAMR_URI + "food";
const GET_FOOD_FEEDING_DATA_URI = BASE_COMMON_API_URI + BASE_FAMR_URI + "food/feedings/";
const ADD_FOOD_FEEDING_DATA_URI = BASE_COMMON_API_URI + BASE_FAMR_URI + "food/feedings/";
const GET_USER_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "users";
const DEACTIVE_USER_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/deactive/";
const GET_USER_DETAILS_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/";
const GET_ROLE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "role";
const USER_PASS_CHANGE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "password/";
const USER_UPDATE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "users/update/";
const CHANGE_ROLE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/role/";
