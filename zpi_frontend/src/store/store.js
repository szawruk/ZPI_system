import { createStore } from "vuex"
import main from './main';

const store = createStore({
    strict: true,
        modules:{
            main
        }
})
export default store;

