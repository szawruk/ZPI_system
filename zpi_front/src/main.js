import Vue from 'vue'
import store from './store/store'
import App from './App'
import router from './router/index'
import 'vue-select/dist/vue-select.css';

new Vue({
    render: h => h(App),
    router,
    store,
}).$mount('#app')

