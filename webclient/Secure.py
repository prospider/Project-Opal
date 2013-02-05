__author__ = 'Gary'

import hashlib



def SecureIt(insecureString):
    hasher = hashlib.sha512()
    hasher.update(insecureString.encode('utf-8'))
    print(hasher.hexdigest())
    return hasher.hexdigest()

