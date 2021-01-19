import {createStore} from "vuex"
import main from './main';
import topics from './topics';
import teams from "@/store/teams";

const store = createStore({
    strict: true,
    modules: {
        main,
        topics,
        teams
    }
})
export default store;

