import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import Dashboard from 'components/pages/dashboard'
import Sites from 'components/pages/sites/list'
import AddSite from 'components/pages/sites/add'
import EditSite from 'components/pages/sites/edit'
import AccessList from 'components/pages/access/list'

export const routes = [
  { name: 'dashboard', path: '/', component: Dashboard, display: 'Dashboard', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' },
  { name: 'sites', path: '/sites', component: Sites, display: 'Sites', icon: 'list' },
  { name: 'add-site', path: '/add-site', component: AddSite, display: 'Add Site', hidden: true },
  { name: 'edit-site', path: '/edit-site/:id?', component: EditSite, display: 'Edit Site', hidden: true },
  { name: 'access-list', path: '/access-list', component: AccessList, display: 'Access List', icon: 'list' },
]
