import Vuex from "vuex"
import main from './main';
import topics from './topics';
import teams from "@/store/teams";
import Vue from 'vue'

Vue.use(Vuex)

const store = new Vuex.Store({
    strict: true,
    modules: {
        main,
        topics,
        teams
    }
})
export default store;

