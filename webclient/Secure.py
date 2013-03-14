__author__ = 'Gary'

import hashlib



def SecureIt(insecureString):
    hasher = hashlib.sha512()
    hasher.update(insecureString.encode('utf-8'))
    print(hasher.hexdigest())
    return hasher.hexdigest()

def Compare(s1, s2):
    fs1 = s1.replace("-","").lower()
    fs2 = s2.replace("-","").lower()
    if fs1 == fs2:
        return True
    else:
        return False