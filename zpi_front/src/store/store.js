import Vuex from "vuex"
import main from './main';
import topics from './topics';
import teams from "@/store/teams";
import myTeam from "@/store/myTeam";
import Vue from 'vue'
import axios from "axios";
import {axiosBaseUrl} from "@/config/config";
axios.defaults.baseURL = axiosBaseUrl

Vue.use(Vuex)

const store = new Vuex.Store({
    strict: true,
    modules: {
        main,
        topics,
        teams,
        myTeam
    }
})
export default store;

