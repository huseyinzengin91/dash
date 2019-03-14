import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/root/app-root'
import { FontAwesomeIcon } from './icons'
import './filters.js'

import Datetime from 'vue-datetime'
Vue.use(Datetime)
import 'vue-datetime/dist/vue-datetime.css'

import { Settings } from 'luxon'
Settings.defaultLocale = 'en'

import Notifications from 'vue-notification'
Vue.use(Notifications)


// Registration of global components
Vue.component('icon', FontAwesomeIcon)
Vue.prototype.$http = axios

sync(store, router)

const app = new Vue({
  store,
  router,
  ...App
})

export {
  app,
  router,
  store
}