import Vuex from "vuex"
import main from './main';
import topics from './topics';
import teams from "@/store/teams";
import myTeam from "@/store/myTeam";
import Vue from 'vue'

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

