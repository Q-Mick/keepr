import { reactive } from "vue";

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Account.js').Profile | null} */
  activeProfile:{},
  profileVaults: [],
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  
  /** @type {import('./models/Keep.js').Keep{}} */
  actKeep: {},
  
activeVaultKeep:{},
  /** @type {import('./models/Keep.js').Keep[] | null} */
  myKeeps: [],
  /** @type {import('./models/Keep.js').Keep[] | null} */
  profileKeeps:[],

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/Vault.js').Vault{}} */
  actVault: {},

 
  /** @type {import('./models/Vault.js').Vault[] | null} */
  myVaults: [],

  /** @type {import('./models/Keep.js').Keep[]} */
  vaultKeeps: [],
});
