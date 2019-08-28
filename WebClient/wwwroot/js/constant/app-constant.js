const GET = "GET";
const POST = "POST";
const PUT = "PUT";
const DELETE = "DELETE";
const JSON_DATATYPE = "JSON";

const BASE_COMMON_API_URI = "http://localhost:4201/";

const BASE_ADMIN_URI = BASE_COMMON_API_URI + "api/admin/";
const BASE_PROVIDER_URI = "api/provider/";
const BASE_FAMR_URI = BASE_COMMON_API_URI + "api/farm/";
const BASE_STAFF_URI = "api/staff/";
const BASE_COMMON_URI = BASE_COMMON_API_URI + "api/common/";
const BASE_GUEST_URI = BASE_COMMON_API_URI + "api/guest/";
const BASE_REGISTER_URI = BASE_COMMON_API_URI+ "api/register/";
const BASE_MANAGER_URI = "api/manager/";
const BASE_USER_URI = BASE_COMMON_API_URI + "api/user/"
const BASE_DISTRIBUTOR_URI = BASE_COMMON_API_URI + "api/distributor/";

//Guest
const GET_FOODDATA_BY_ID_URI = BASE_GUEST_URI + "foodData";
const ACTIVATE_URI = BASE_GUEST_URI + "account/activate/";
const REGISTER_URI = BASE_GUEST_URI + "register";
const FORGET_PASSWORD_URI = BASE_GUEST_URI + "forgetPassword";
const GET_TRANSACTION_URI = BASE_GUEST_URI + "transactions";
const GET_TRANSACTION_INPUT_URI = BASE_GUEST_URI + "transactions/";

//Farm
const GET_FOOD_DETAIL_TYPE_URI = BASE_FAMR_URI + "productdetailtype";
const GET_FOOD_CATEGORY_URI = BASE_FAMR_URI + "category";
const GET_FARM_FOOD_URI = BASE_FAMR_URI + "foods";
const CREATE_FOOD_DATA_URI = BASE_FAMR_URI + "food";
const GET_FOOD_FEEDING_DATA_URI = BASE_FAMR_URI + "food/feedings/";
const ADD_FOOD_FEEDING_DATA_URI = BASE_FAMR_URI + "food/feedings/";
const GET_FOOD_VACCIN_DATA_URI = BASE_FAMR_URI + "food/vaccinations/";
const ADD_FOOD_VACCIN_DATA_URI = BASE_FAMR_URI + "food/vaccinations/";
const COUNT_FARM_TRANSACTION_URI = BASE_FAMR_URI + "countFarmTransaction"
const GET_FARM_TRANSACTION_URI = BASE_FAMR_URI + "getAllFarmTransaction";
const CREATE_TRANSACTION_URI = BASE_FAMR_URI + "createTransaction";
const GET_PROVIDER = BASE_FAMR_URI + "getAllProvider";
const UPDATE_FEEDING_URI = BASE_FAMR_URI + "feedings";
const GET_FEEDING_LIST_BY_FOODID_URI = BASE_FAMR_URI + "feedings/";
const GET_FEEDING_LIST_BY_PREMISES_URI = BASE_FAMR_URI + "premisesFeedings";
const REMOVE_FEEDING_URI = BASE_FAMR_URI + "feeding/";
const UPDATE_VACCINE_URI = BASE_FAMR_URI + "vaccines";
const GET_VACCINE_LIST_URI = BASE_FAMR_URI + "vaccines";
const GET_VACCINE_LIST_BY_PREMISES_URI = BASE_FAMR_URI + "premisesVaccines";
const REMOVE_VACCINE_URI = BASE_FAMR_URI + "vaccine/";
const DOWNLOAD_REPORT_URI = BASE_FAMR_URI + "farmReport";
const SOLD_OUT_URI = BASE_FAMR_URI + "soldOut/";


//Admin
const GET_USER_URI = BASE_ADMIN_URI + "users";
const DEACTIVE_USER_URI = BASE_ADMIN_URI + "user/deactive/";
const GET_USER_DETAILS_URI = BASE_ADMIN_URI + "user/";
const GET_ROLE_URI = BASE_ADMIN_URI + "role";
const CHANGE_ROLE_URI = BASE_ADMIN_URI + "user/role/";
const GET_PREMISES_URI = BASE_ADMIN_URI + "premises";
const UPDATE_PREMISES_STATUS_URI = BASE_ADMIN_URI + "premises/status/";
const CREATE_NEW_PREMISES_URI = BASE_REGISTER_URI + "premises";
const CREATE_VETERINARY_URI = BASE_ADMIN_URI + "veterinary";

// Provider
const GET_FOOD_PROVIDER_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getFoodByProvider";
const COUNT_PROVIDER_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "countProviderTransaction";
const GET_PROVIDER_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getAllProviderReceiveTransaction";
const UPDATE_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "UpdateTransaction/";
const CREATE_PROVIDER_FOOD_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "providerFood";
const GET_PROVIDER_FOOD_DETAIL_TYPE_URI = BASE_COMMON_URI + "productproviderdetailtype";
const ADD_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "treatment";
const GET_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "foodTreatment/"
const ADD_MORE_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "moreTreatment/";
const GET_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "treatment";
const UPDATE_FOOD_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "food/treatment/";
const DELETE_TREATMENT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "deleteTreatment/"
const GET_DISTRIBUTOR_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getAllDistributor";
const ADD_FOOD_PACKAGING_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "food/packaging/";
const PROVIDER_GET_FOOD_DATA_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getFoodDataByProvider";
const PROVIDER_CREATE_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "createTransaction";
const PROVIDER_GET_SEND_TRANSACTION_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "getAllProviderSendTransaction";
const PROVIDER_DOWNLOAD_REPORT_URI = BASE_COMMON_API_URI + BASE_PROVIDER_URI + "downloadReport";

//Manager
const MANAGER_GET_ACCOUNT_URI = BASE_COMMON_API_URI + BASE_MANAGER_URI + "getUserByPremises";
const MANAGER_UPDATE_ACCOUNT_STATUS_URI = BASE_COMMON_API_URI + BASE_MANAGER_URI + "changeUserStatus/"
const MANAGER_CREATE_ACCOUNT_URI = BASE_COMMON_API_URI + BASE_MANAGER_URI + "premise/account";

//User
const GET_PROFILE_URI = BASE_USER_URI + "profile";
const USER_PASS_CHANGE_URI = BASE_USER_URI + "password";
const CHANGE_AVA_URI = BASE_USER_URI + "user/avatar";
const USER_UPDATE_URI = BASE_USER_URI + "users/update";

//Distributor
const DISTRIBUTOR_GET_FOOD = BASE_DISTRIBUTOR_URI + "getFoodByDistributor";