const GET = "GET";
const POST = "POST";
const PUT = "PUT";
const JSON_DATATYPE = "JSON";


const BASE_ADMIN_API_URI = "https://localhost:4200/";
const BASE_COMMON_API_URI = "https://localhost:4201/";

const BASE_ADMIN_URI = "api/admin/";
const BASE_PROVIDER_URI = "api/provider/";
const BASE_FAMR_URI = BASE_COMMON_API_URI + "api/farm/";
const BASE_STAFF_URI = "api/staff/";
const BASE_GUEST_URI = "api/guest/";
const BASE_REGISTER_URI = "api/register/";
const BASE_COMMON_URI = "api/common/";

//Guest
const LOGIN_URI = BASE_COMMON_API_URI + BASE_GUEST_URI + "login";


//Farm
const GET_FOOD_DETAIL_TYPE_URI      = BASE_FAMR_URI + "productdetailtype";
const GET_FOOD_CATEGORY_URI         = BASE_FAMR_URI + "category";
const GET_FARM_FOOD_URI             = BASE_FAMR_URI + "foods";
const CREATE_FOOD_DATA_URI          = BASE_FAMR_URI + "food";
const GET_FOOD_FEEDING_DATA_URI     = BASE_FAMR_URI + "food/feedings/";
const ADD_FOOD_FEEDING_DATA_URI = BASE_FAMR_URI + "food/feedings/";
const GET_FOOD_VACCIN_DATA_URI = BASE_FAMR_URI + "food/vaccinations/";
const ADD_FOOD_VACCIN_DATA_URI = BASE_FAMR_URI + "food/vaccinations/";
const COUNT_FARM_TRANSACTION_URI    = BASE_FAMR_URI + "countFarmTransaction"
const GET_FARM_TRANSACTION_URI      = BASE_FAMR_URI + "getAllFarmTransaction";
const CREATE_TRANSACTION_URI = BASE_FAMR_URI + "createTransaction";
const GET_PROVIDER = BASE_FAMR_URI + "getAllProvider";


//Admin
const GET_USER_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "users";
const DEACTIVE_USER_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/deactive/";
const GET_USER_DETAILS_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/";
const GET_ROLE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "role";
const USER_PASS_CHANGE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "password/";
const USER_UPDATE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "users/update/";
const CHANGE_ROLE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "user/role/";
const GET_PREMISES_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "premises";
const UPDATE_PREMISES_STATUS_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "premises/status/";
const CREATE_NEW_PREMISES_URI = BASE_COMMON_API_URI + BASE_REGISTER_URI + "premises";
const GET_PROFILE_URI = BASE_ADMIN_API_URI + BASE_ADMIN_URI + "profile";

// Provider
const GET_FOOD_PROVIDER_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getFoodByProvider";
const COUNT_PROVIDER_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "countProviderTransaction";
const GET_PROVIDER_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getAllProviderTransaction";
const UPDATE_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "UpdateTransaction/";
const CREATE_PROVIDER_FOOD_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "providerFood";
const GET_PROVIDER_FOOD_DETAIL_TYPE_URI =  BASE_COMMON_URI + "productproviderdetailtype";
const ADD_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "treatment/";
const GET_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "foodTreatment/"
const ADD_MORE_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "moreTreatment/";